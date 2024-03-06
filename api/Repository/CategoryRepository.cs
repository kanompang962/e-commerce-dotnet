using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Category;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _context;
        public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public Task<bool> CategoryExists(int id)
        {
            return _context.Categorys.AnyAsync(c => c.Id == id);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _context.Categorys.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteAsync(int id)
        {
            var existingCategory = await _context.Categorys.FirstOrDefaultAsync(c => c.Id == id);
            if(existingCategory == null)
                return null;

            _context.Categorys.Remove(existingCategory);
            await _context.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var category = await _context.Categorys.ToListAsync();
            return category;
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            var category = await _context.Categorys.FirstOrDefaultAsync(c => c.Id == id);
            if(category == null)
                return null;

            return category;
        }

        public async Task<Category?> UpdateAsync(int id, CategoryDtoUpdate categoryDto)
        {
            var existingCategory = await _context.Categorys.FirstOrDefaultAsync(c => c.Id == id);
            if(existingCategory == null)
                return null;
            
            existingCategory.Name = categoryDto.Name;
            await _context.SaveChangesAsync();
            return existingCategory;
        }
    }
}