using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.DTOs;
using MyApp.WebAPI.Models; 
using MyApp.WebAPI.Services;

namespace MyApp.WebAPI.Controllers
{
    /// <summary>
    /// Controller untuk mengelola data Kategori
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        /// <summary>
        /// Mengambil semua data kategori
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<CategoryDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(ApiResponse<IEnumerable<CategoryDto>>.SuccessResult(categories));
        }

        /// <summary>
        /// Mengambil data kategori berdasarkan ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category is null)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"Kategori dengan ID {id} tidak ditemukan."));
            }

            return Ok(ApiResponse<CategoryDto>.SuccessResult(category));
        }

        /// <summary>
        /// Membuat kategori baru
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var category = await _categoryService.CreateCategoryAsync(createCategoryDto);
            var response = ApiResponse<CategoryDto>.SuccessResult(category, "Kategori berhasil dibuat.");
            
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, response);
        }

        /// <summary>
        /// Memperbarui data kategori
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _categoryService.UpdateCategoryAsync(id, updateCategoryDto);

            if (category is null)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"Kategori dengan ID {id} tidak ditemukan."));
            }
            
            var response = ApiResponse<CategoryDto>.SuccessResult(category, "Kategori berhasil diperbarui.");
            return Ok(response);
        }

        /// <summary>
        /// Menghapus data kategori
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);

            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"Kategori dengan ID {id} tidak ditemukan."));
            }

            return NoContent();
        }
    }
}