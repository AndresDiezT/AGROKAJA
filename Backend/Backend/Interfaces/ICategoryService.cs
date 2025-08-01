using Backend.DTOs.CategoryDTOs;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<Category> UpdateCategoryAsync(int id, CreateCategoryDto updateCategoryDto);
        Task<bool> DeactiveCategoryAsync(int id);
        Task<bool> ActiveCategoryAsync(int id);
    }
}
