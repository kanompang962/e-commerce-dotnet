using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Product;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IFileService _fileService;
        private readonly ICategoryRepository _categoryRepo;
        public ProductRepository(ApplicationDBContext context, ICategoryRepository categoryRepo, IFileService fileService)
        {
            _context = context;
            _categoryRepo = categoryRepo;
            _fileService = fileService;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if(product == null)
                return null;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var products = await _context.Products.Include(c => c.Category).ToListAsync();
            return products;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var product = await _context.Products.Include(c => c.Category).FirstOrDefaultAsync(p => p.Id == id);
            
            if(product == null)
                return null;

            return product;
        }

        public async Task<Product?> UpdateAsync(int id, ProductDtoUpdateFormData productDto)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            var existingCategoryId = await _categoryRepo.CategoryExists(productDto.categoryId);
            var dbPath = "";

            if(existingProduct == null)
                return null;

            if(!existingCategoryId)
                return null;

            if(productDto.Img == null)
                return null;
            
            dbPath = _fileService.UploadImage(productDto.Img, "products");     
            
            existingProduct.Name = productDto.Name;
            existingProduct.Img = dbPath!;
            existingProduct.Price = productDto.Price;
            existingProduct.Quantity = productDto.Quantity;
            existingProduct.Description = productDto.Description;
            existingProduct.CategoryId = productDto.categoryId;

            await _context.SaveChangesAsync();
            return existingProduct;
        }
    }
}