using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Models; // Diperlukan untuk ApiResponse
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// Controller untuk mengelola data Menu Course
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MenuCoursesController : ControllerBase
    {
        private readonly IMenuCourseService _menuCourseService;
        private readonly ILogger<MenuCoursesController> _logger;

        public MenuCoursesController(IMenuCourseService menuCourseService, ILogger<MenuCoursesController> logger)
        {
            _menuCourseService = menuCourseService;
            _logger = logger;
        }

        /// <summary>
        /// Mengambil semua data menu course
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<MenuCourseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _menuCourseService.GetAllMenuCoursesAsync();
            // DIPERBAIKI: Menggunakan MenuCourseDto (tunggal)
            return Ok(ApiResponse<IEnumerable<MenuCourseDto>>.SuccessResult(courses));
        }

        /// <summary>
        /// Mengambil data menu course berdasarkan ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<MenuCourseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _menuCourseService.GetMenuCourseByIdAsync(id);
            if (course is null)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"MenuCourse dengan ID {id} tidak ditemukan."));
            }
            // DIPERBAIKI: Menggunakan MenuCourseDto (tunggal)
            return Ok(ApiResponse<MenuCourseDto>.SuccessResult(course));
        }

        /// <summary>
        /// Membuat menu course baru
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<MenuCourseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateMenuCourseDto createDto)
        {
            // Pola try-catch ini bagus untuk menangani error bisnis spesifik
            try
            {
                var newCourse = await _menuCourseService.CreateMenuCourseAsync(createDto);
                // DIPERBAIKI: Menggunakan MenuCourseDto (tunggal)
                var response = ApiResponse<MenuCourseDto>.SuccessResult(newCourse, "MenuCourse berhasil dibuat.");
                return CreatedAtAction(nameof(GetById), new { id = newCourse.Id }, response);
            }
            catch (ArgumentException ex)
            {
                // Menangkap error validasi dari service (misal: CategoryId tidak ada)
                return BadRequest(ApiResponse<object>.ErrorResult(ex.Message));
            }
            // catch(Exception) umum dihapus, biarkan middleware yang menangani
        }

        /// <summary>
        /// Memperbarui menu course
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<MenuCourseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMenuCourseDto updateDto)
        {
            try
            {
                var updatedCourse = await _menuCourseService.UpdateMenuCourseAsync(id, updateDto);
                if (updatedCourse is null)
                {
                    return NotFound(ApiResponse<object>.ErrorResult($"MenuCourse dengan ID {id} tidak ditemukan."));
                }
                // DIPERBAIKI: Menggunakan MenuCourseDto (tunggal)
                var response = ApiResponse<MenuCourseDto>.SuccessResult(updatedCourse, "MenuCourse berhasil diperbarui.");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResult(ex.Message));
            }
        }

        /// <summary>
        /// Menghapus menu course
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _menuCourseService.DeleteMenuCourseAsync(id);
            if (!success)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"MenuCourse dengan ID {id} tidak ditemukan."));
            }
            return NoContent();
        }
    }
}