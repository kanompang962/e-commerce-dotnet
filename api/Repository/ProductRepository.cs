using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _context;
        public ProductRepository(ApplicationDBContext context)
        {
            _context = context;
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
    }
}