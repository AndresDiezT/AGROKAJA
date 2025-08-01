using Backend.Data;
using Backend.DTOs.CategoryDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly BackendDbContext _context;
        public CategoryService(BackendDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"La categoria con el ID {id} no Existe.");
            }return category;
        }
        public async Task<Category> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
           var categoryExists = await _context.Categories
                .AnyAsync(c => c.NameCategory == createCategoryDto.NameCategory);
            if (categoryExists)
            {
               throw new InvalidOperationException($"La categoría '{createCategoryDto.NameCategory}' ya existe.");
            }
              var category = new Category
                {
                 NameCategory = createCategoryDto.NameCategory,
                  CreatedAt = DateTime.UtcNow,
                 IsActive = true
                };
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return category;
        }
        public async Task<Category> UpdateCategoryAsync(int id, CreateCategoryDto updateCategoryDto)
        {
           var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"La categoría con el ID {id} no existe.");
            }
            var categoryExists = await _context.Categories
                .AnyAsync(c => c.NameCategory == updateCategoryDto.NameCategory && c.IdCategory != id);
            if (categoryExists)
            {
                throw new InvalidOperationException($"La categoría '{updateCategoryDto.NameCategory}' ya existe.");
            }
            category.NameCategory = updateCategoryDto.NameCategory;
            category.UpdatedAt = DateTime.UtcNow;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task<bool> DeactiveCategoryAsync(int id)
        {
           var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"La categoría con el ID {id} no existe.");
            }
            if (!category.IsActive)
            {
                throw new InvalidOperationException($"La categoría con el ID {id} ya está desactivada.");
            }
            category.IsActive = false;
            category.DeactivatedAt = DateTime.UtcNow;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ActiveCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"La categoría con el ID {id} no existe.");
            }
            if (category.IsActive)
            {
                throw new InvalidOperationException($"La categoría con el ID {id} ya está activa.");
            }
            category.IsActive = true;
            category.DeactivatedAt = null;
            category.UpdatedAt = DateTime.UtcNow;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
