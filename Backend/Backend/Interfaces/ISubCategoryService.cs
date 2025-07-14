using Backend.DTOs.CategoryDTOs;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface ISubCategoryService
    {
        Task<IEnumerable<SubCategory>> GetAllSubCategoriesAsync();
        Task<SubCategory> GetSubCategoryByIdAsync(int id);
        Task<SubCategory> CreateSubCategoryAsync(CreateSubCategoryDto createSubCategoryDto);
        Task<SubCategory> UpdateSubCategoryAsync(int id, CreateSubCategoryDto updateSubCategoryDto);
        Task<bool> DeactiveSubCategoryAsync(int id);
        Task<bool> ActiveSubCategoryAsync(int id);
    }
}
