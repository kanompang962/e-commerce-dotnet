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
              private readonly ICategoryRepository _categoryRepo;
        public ProductRepository(ApplicationDBContext context, ICategoryRepository categoryRepo)
        {
            _context = context;
            _categoryRepo = categoryRepo;
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

        public async Task<Product?> UpdateAsync(int id, ProductDtoUpdate productDto)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            var existingCategoryId = await _categoryRepo.CategoryExists(productDto.categoryId);
            if(existingProduct == null)
                return null;
            if(!existingCategoryId)
                return null;
            
            existingProduct.Name = productDto.Name;
            existingProduct.Img = productDto.Img;
            existingProduct.Price = productDto.Price;
            existingProduct.Quantity = productDto.Quantity;
            existingProduct.Description = productDto.Description;
            existingProduct.CategoryId = productDto.categoryId;

            await _context.SaveChangesAsync();
            return existingProduct;
        }
    }
}