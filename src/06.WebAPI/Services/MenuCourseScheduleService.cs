using AutoMapper;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.DTOs;
using MyApp.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MyApp.WebAPI.Services
{
    public class MenuCourseScheduleService : IMenuCourseScheduleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<MenuCourseScheduleService> _logger;

        public MenuCourseScheduleService(ApplicationDbContext context, IMapper mapper, ILogger<MenuCourseScheduleService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<MenuCourseScheduleDto>> GetSchedulesForCourseAsync(int menuCourseId)
        {
            var assignments = await _context.MenuCourse_Schedules
                .Where(mcs => mcs.MenuCourseId == menuCourseId)
                // Wajib Include() agar AutoMapper bisa mengambil data dari tabel relasi
                .Include(mcs => mcs.MenuCourse)
                .Include(mcs => mcs.Schedule)
                .ToListAsync();
            
            return _mapper.Map<IEnumerable<MenuCourseScheduleDto>>(assignments);
        }

        public async Task<MenuCourseScheduleDto> AssignScheduleAsync(CreateMenuCourseScheduleDto createDto)
        {
            // Validasi: Pastikan course dan jadwal yang dituju benar-benar ada
            var courseExists = await _context.MenuCourses.AnyAsync(c => c.Id == createDto.MenuCourseId);
            var scheduleExists = await _context.Schedules.AnyAsync(s => s.Id == createDto.ScheduleId);

            if (!courseExists) throw new ArgumentException($"MenuCourse dengan ID {createDto.MenuCourseId} tidak ditemukan.");
            if (!scheduleExists) throw new ArgumentException($"Schedule dengan ID {createDto.ScheduleId} tidak ditemukan.");

            // Validasi: Cegah duplikasi pendaftaran jadwal yang sama untuk course yang sama
            var alreadyExists = await _context.MenuCourse_Schedules
                .AnyAsync(mcs => mcs.MenuCourseId == createDto.MenuCourseId && mcs.ScheduleId == createDto.ScheduleId);
            if (alreadyExists) throw new ArgumentException("Jadwal ini sudah terdaftar untuk course tersebut.");

            var newAssignment = _mapper.Map<MenuCourse_Schedule>(createDto);

            _context.MenuCourse_Schedules.Add(newAssignment);
            await _context.SaveChangesAsync();
            
            // Reload data relasi agar bisa di-map dengan lengkap ke DTO
            var result = await _context.MenuCourse_Schedules
                .Include(mcs => mcs.MenuCourse)
                .Include(mcs => mcs.Schedule)
                .FirstAsync(mcs => mcs.Id == newAssignment.Id);

            _logger.LogInformation("Jadwal {ScheduleId} berhasil didaftarkan ke Course {MenuCourseId}", createDto.ScheduleId, createDto.MenuCourseId);
            return _mapper.Map<MenuCourseScheduleDto>(result);
        }

        public async Task<MenuCourseScheduleDto?> UpdateAssignmentAsync(int id, UpdateMenuCourseScheduleDto updateDto)
        {
            var assignment = await _context.MenuCourse_Schedules
                .Include(mcs => mcs.MenuCourse)
                .Include(mcs => mcs.Schedule)
                .FirstOrDefaultAsync(mcs => mcs.Id == id);
                
            if (assignment == null) return null;

            // Map perubahan dari DTO ke entitas yang sudah ada
            _mapper.Map(updateDto, assignment);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Pendaftaran jadwal dengan ID {AssignmentId} telah diperbarui.", id);
            return _mapper.Map<MenuCourseScheduleDto>(assignment);
        }

        public async Task<bool> UnassignScheduleAsync(int id)
        {
            var assignment = await _context.MenuCourse_Schedules.FindAsync(id);
            if (assignment == null) return false;

            _context.MenuCourse_Schedules.Remove(assignment);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Pendaftaran jadwal dengan ID {AssignmentId} telah dihapus.", id);
            return true;
        }
    }
}