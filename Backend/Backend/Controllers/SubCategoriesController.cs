using Backend.DTOs.CategoryDTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubCategoriesController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;
        public SubCategoriesController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSubCategories()
        {
            var subCategories = await _subCategoryService.GetAllSubCategoriesAsync();
            return Ok(subCategories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubCategoryById(int id)
        {
            try
            {
                var subCategory = await _subCategoryService.GetSubCategoryByIdAsync(id);
                return Ok(subCategory);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateSubCategory([FromBody] CreateSubCategoryDto createSubCategoryDto)
        {
            try
            {
                var subCategory = await _subCategoryService.CreateSubCategoryAsync(createSubCategoryDto);
                return CreatedAtAction(nameof(GetSubCategoryById), new { id = subCategory.IdSubCategory }, subCategory);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubCategory(int id, [FromBody] CreateSubCategoryDto updateSubCategoryDto)
        {
            try
            {
                var updatedSubCategory = await _subCategoryService.UpdateSubCategoryAsync(id, updateSubCategoryDto);
                return Ok(updatedSubCategory);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateSubCategory(int id)
        {
            try
            {
                var result = await _subCategoryService.DeactiveSubCategoryAsync(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound($"SubCategory with ID {id} not found.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateSubCategory(int id)
        {
            try
            {
                var result = await _subCategoryService.ActiveSubCategoryAsync(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound($"SubCategory with ID {id} not found.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
