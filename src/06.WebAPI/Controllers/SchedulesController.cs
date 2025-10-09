using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// Controller untuk mengelola data master Jadwal (Schedules)
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly ILogger<SchedulesController> _logger;

        public SchedulesController(IScheduleService scheduleService, ILogger<SchedulesController> logger)
        {
            _scheduleService = scheduleService;
            _logger = logger;
        }

        /// <summary>
        /// Mengambil semua data jadwal yang tersedia
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ScheduleDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var schedules = await _scheduleService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<ScheduleDto>>.SuccessResult(schedules));
        }

        /// <summary>
        /// Membuat jadwal baru
        /// </summary>
        /// <param name="createDto">Data untuk jadwal baru</param>
        /// <returns>Jadwal yang baru dibuat</returns>
        /// <response code="201">Berhasil membuat jadwal</response>
        /// <response code="400">Input tidak valid (misal: jadwal sudah ada)</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<ScheduleDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateScheduleDto createDto)
        {
            try
            {
                var newSchedule = await _scheduleService.CreateAsync(createDto);
                var response = ApiResponse<ScheduleDto>.SuccessResult(newSchedule, "Jadwal berhasil dibuat.");
                // Menggunakan CreatedAtAction membutuhkan endpoint GetById, yang tidak ada di service ini.
                // Mengembalikan 201 Created dengan lokasi header ke resource baru adalah praktik yang baik,
                // tapi untuk kesederhanaan, kita bisa kembalikan 200 OK atau 201 dengan objeknya.
                return Ok(response); 
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResult(ex.Message));
            }
        }

        /// <summary>
        /// Menghapus sebuah jadwal
        /// </summary>
        /// <param name="id">ID dari jadwal yang akan dihapus</param>
        /// <response code="204">Berhasil menghapus jadwal</response>
        /// <response code="404">Jadwal tidak ditemukan</response>
        /// <response code="409">Jadwal tidak bisa dihapus karena masih digunakan</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _scheduleService.DeleteAsync(id);
                if (!success)
                {
                    return NotFound(ApiResponse<object>.ErrorResult($"Jadwal dengan ID {id} tidak ditemukan."));
                }
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                // 409 Conflict adalah status yang tepat ketika sebuah aksi tidak bisa dilakukan karena konflik state
                return Conflict(ApiResponse<object>.ErrorResult(ex.Message));
            }
        }
    }
}