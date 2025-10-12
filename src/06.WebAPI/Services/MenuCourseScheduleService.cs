// Lokasi: src/06.WebAPI/Services/MenuCourseScheduleService.cs

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
            // PERBAIKAN: Menggunakan nama DbSet yang benar -> MenuCourseSchedules
            var assignments = await _context.MenuCourseSchedules
                .Where(mcs => mcs.MenuCourseId == menuCourseId)
                .Include(mcs => mcs.MenuCourse)
                .Include(mcs => mcs.Schedule)
                .ToListAsync();
            
            return _mapper.Map<IEnumerable<MenuCourseScheduleDto>>(assignments);
        }

        public async Task<MenuCourseScheduleDto> AssignScheduleAsync(CreateMenuCourseScheduleDto createDto)
        {
            // PERBAIKAN: Menggunakan nama properti ID yang benar
            var course = await _context.MenuCourses.FindAsync(createDto.MenuCourseId);
            var schedule = await _context.Schedules.FindAsync(createDto.ScheduleId);

            if (course == null)
            {
                throw new ArgumentException($"MenuCourse dengan ID {createDto.MenuCourseId} tidak ditemukan.");
            }
            if (schedule == null)
            {
                throw new ArgumentException($"Schedule dengan ID {createDto.ScheduleId} tidak ditemukan.");
            }

            var assignment = _mapper.Map<MenuCourseSchedule>(createDto);
            
            // PERBAIKAN: Menggunakan nama DbSet yang benar -> MenuCourseSchedules
            _context.MenuCourseSchedules.Add(assignment);
            await _context.SaveChangesAsync();
            
            var result = await _context.MenuCourseSchedules
                .Include(mcs => mcs.MenuCourse)
                .Include(mcs => mcs.Schedule)
                .FirstAsync(mcs => mcs.MSId == assignment.MSId);

            _logger.LogInformation("Jadwal {ScheduleId} berhasil didaftarkan ke Course {MenuCourseId}", createDto.ScheduleId, createDto.MenuCourseId);
            return _mapper.Map<MenuCourseScheduleDto>(result);
        }

        public async Task<MenuCourseScheduleDto?> UpdateAssignmentAsync(int id, UpdateMenuCourseScheduleDto updateDto)
        {
            // PERBAIKAN: Menggunakan nama DbSet yang benar -> MenuCourseSchedules
            var assignment = await _context.MenuCourseSchedules
                .Include(mcs => mcs.MenuCourse)
                .Include(mcs => mcs.Schedule)
                .FirstOrDefaultAsync(mcs => mcs.MSId == id);
                
            if (assignment == null) return null;

            _mapper.Map(updateDto, assignment);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Pendaftaran jadwal dengan ID {AssignmentId} telah diperbarui.", id);
            return _mapper.Map<MenuCourseScheduleDto>(assignment);
        }

        public async Task<bool> UnassignScheduleAsync(int id)
        {
            // PERBAIKAN: Menggunakan nama DbSet yang benar -> MenuCourseSchedules
            var assignment = await _context.MenuCourseSchedules.FindAsync(id);
            if (assignment == null) return false;

            // PERBAIKAN: Menggunakan nama DbSet yang benar -> MenuCourseSchedules
            _context.MenuCourseSchedules.Remove(assignment);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Pendaftaran jadwal dengan ID {AssignmentId} telah dihapus.", id);
            return true;
        }
    }
}