using Microsoft.AspNetCore.Mvc;
usingMyApp.WebAPI.DTOs;
usingMyApp.WebAPI.Models;
usingMyApp.WebAPI.Services;

namespaceMyApp.WebAPI.Controllers
{
    /// <summary>
    /// Controller untuk mengelola jadwal spesifik untuk setiap Menu Course
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MenuCourseSchedulesController : ControllerBase
    {
        private readonly IMenuCourseScheduleService _mcsService;

        public MenuCourseSchedulesController(IMenuCourseScheduleService mcsService)
        {
            _mcsService = mcsService;
        }

        /// <summary>
        /// Mengambil semua jadwal yang terdaftar untuk sebuah course tertentu
        /// </summary>
        /// <param name="menuCourseId">ID dari MenuCourse</param>
        [HttpGet("/api/menucourses/{menuCourseId}/schedules")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<MenuCourseScheduleDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSchedulesForCourse(int menuCourseId)
        {
            var schedules = await _mcsService.GetSchedulesForCourseAsync(menuCourseId);
            return Ok(ApiResponse<IEnumerable<MenuCourseScheduleDto>>.SuccessResult(schedules));
        }

        /// <summary>
        /// Menambahkan sebuah jadwal ke sebuah course
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<MenuCourseScheduleDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AssignSchedule([FromBody] CreateMenuCourseScheduleDto createDto)
        {
            try
            {
                var newAssignment = await _mcsService.AssignScheduleAsync(createDto);
                var response = ApiResponse<MenuCourseScheduleDto>.SuccessResult(newAssignment, "Jadwal berhasil ditambahkan ke course.");
                return Ok(response); // Mengembalikan 200 OK dengan data yang baru dibuat
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResult(ex.Message));
            }
        }

        /// <summary>
        /// Memperbarui detail pendaftaran jadwal (misal: slot atau status)
        /// </summary>
        /// <param name="id">ID dari pendaftaran jadwal (MenuCourse_Schedule ID)</param>
        /// <param name="updateDto">Data yang akan diupdate</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<MenuCourseScheduleDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAssignment(int id, [FromBody] UpdateMenuCourseScheduleDto updateDto)
        {
            var updatedAssignment = await _mcsService.UpdateAssignmentAsync(id, updateDto);

            if (updatedAssignment is null)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"Pendaftaran jadwal dengan ID {id} tidak ditemukan."));
            }

            var response = ApiResponse<MenuCourseScheduleDto>.SuccessResult(updatedAssignment, "Detail jadwal berhasil diperbarui.");
            return Ok(response);
        }

        /// <summary>
        /// Menghapus pendaftaran sebuah jadwal dari sebuah course
        /// </summary>
        /// <param name="id">ID dari pendaftaran jadwal (MenuCourse_Schedule ID)</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UnassignSchedule(int id)
        {
            var success = await _mcsService.UnassignScheduleAsync(id);

            if (!success)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"Pendaftaran jadwal dengan ID {id} tidak ditemukan."));
            }

            return NoContent();
        }
    }
}