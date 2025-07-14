using Backend.Data;
using Backend.DTOs.CategoryDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly BackendDbContext _context;
        public SubCategoryService(BackendDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SubCategory>> GetAllSubCategoriesAsync()
        {
            var subCategories = await _context.SubCategories.ToListAsync();
            return subCategories;
        }
        public async Task<SubCategory> GetSubCategoryByIdAsync(int id)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                throw new KeyNotFoundException($"La subcategoría con el ID {id} no existe.");
            }
            return subCategory;
        }
        public async Task<SubCategory> CreateSubCategoryAsync(CreateSubCategoryDto createSubCategoryDto)
        {
            var subCategoryExists = await _context.SubCategories
                .AnyAsync(sc => sc.NameSubCategory == createSubCategoryDto.NameSubCategory);
            if (subCategoryExists)
            {
                throw new InvalidOperationException($"La subcategoría '{createSubCategoryDto.NameSubCategory}' ya existe.");
            }
            var subCategory = new SubCategory
            {
                NameSubCategory = createSubCategoryDto.NameSubCategory,
                IdCategory = createSubCategoryDto.IdCategory,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
            _context.SubCategories.Add(subCategory);
            await _context.SaveChangesAsync();
            return subCategory;
        }
        public async Task<SubCategory> UpdateSubCategoryAsync(int id, CreateSubCategoryDto updateSubCategoryDto)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                throw new KeyNotFoundException($"La subcategoría con el ID {id} no existe.");
            }
            var subCategoryExists = await _context.SubCategories
                .AnyAsync(sc => sc.NameSubCategory == updateSubCategoryDto.NameSubCategory && sc.IdSubCategory != id);
            if (subCategoryExists)
            {
                throw new InvalidOperationException($"La subcategoría '{updateSubCategoryDto.NameSubCategory}' ya existe.");
            }
            subCategory.NameSubCategory = updateSubCategoryDto.NameSubCategory;
            _context.SubCategories.Update(subCategory);
            await _context.SaveChangesAsync();
            return subCategory;
        }
        public async Task<bool> DeactiveSubCategoryAsync(int id)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                throw new KeyNotFoundException($"La subcategoría con el ID {id} no existe.");
            }
            subCategory.IsActive = false;
            _context.SubCategories.Update(subCategory);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ActiveSubCategoryAsync(int id)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                throw new KeyNotFoundException($"La subcategoría con el ID {id} no existe.");
            }
            subCategory.IsActive = true;
            _context.SubCategories.Update(subCategory);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
