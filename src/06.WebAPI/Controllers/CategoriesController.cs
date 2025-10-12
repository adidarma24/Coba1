// Lokasi: src/06.WebAPI/Controllers/CategoriesController.cs
using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.DTOs;
using MyApp.WebAPI.Models; 
using MyApp.WebAPI.Services;

namespace MyApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(new ApiResponse<IEnumerable<CategoryDto>>
            {
                Success = true,
                Data = categories,
                Message = "Berhasil mengambil semua data kategori."
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category is null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Kategori dengan ID {id} tidak ditemukan."
                });
            }
            return Ok(new ApiResponse<CategoryDto>
            {
                Success = true,
                Data = category,
                Message = "Berhasil mengambil data kategori."
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var newCategory = await _categoryService.CreateCategoryAsync(createCategoryDto);
            var response = new ApiResponse<CategoryDto>
            {
                Success = true,
                Data = newCategory,
                Message = "Kategori berhasil dibuat."
            };
            return CreatedAtAction(nameof(GetCategory), new { id = newCategory.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _categoryService.UpdateCategoryAsync(id, updateCategoryDto);
            if (category is null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Kategori dengan ID {id} tidak ditemukan."
                });
            }
            
            return Ok(new ApiResponse<CategoryDto>
            {
                Success = true,
                Data = category,
                Message = "Kategori berhasil diperbarui."
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Kategori dengan ID {id} tidak ditemukan."
                });
            }
            return NoContent();
        }
    }
}