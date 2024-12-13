using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dto.UserTask.Category;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CategoryRepository : CategoryRepositoryInterface
    {
        ApplicationDBContext _context;
        public CategoryRepository(ApplicationDBContext applicationDBContext) {
            _context = applicationDBContext;
        }
        public Task<List<Category>> GetAllAsync()
        {
            return _context.Categories.Include(c => c.Tasks).ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id) {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> CreateAsync(Category category) {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteAsync(int id) {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null) {
                return null;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> CategoryExists(int id) {
           return await _context.Categories.AnyAsync(s => s.Id == id);
        }

        public async Task<Category?> UpdateAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCategory == null)
                {
                    return null;
                }

            existingCategory.Name = updateCategoryDto.name;
            await _context.SaveChangesAsync();

            return existingCategory;
        }     
        
        public async Task<Category?> GetByNameAsync(string name) {
            return  await _context.Categories.FirstOrDefaultAsync(c => c.Name == name); 
           
        }
    }

   
}