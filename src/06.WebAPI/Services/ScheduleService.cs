// Lokasi: src/06.WebAPI/Services/ScheduleService.cs
using AutoMapper;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.DTOs;
using MyApp.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MyApp.WebAPI.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ScheduleService> _logger;

        public ScheduleService(ApplicationDbContext context, IMapper mapper, ILogger<ScheduleService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ScheduleDto>> GetAllAsync()
        {
            var schedules = await _context.Schedules.ToListAsync();
            return _mapper.Map<IEnumerable<ScheduleDto>>(schedules);
        }

        // DITAMBAHKAN: Implementasi GetAsync
        public async Task<ScheduleDto?> GetAsync(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            return _mapper.Map<ScheduleDto>(schedule);
        }

        public async Task<ScheduleDto> CreateAsync(CreateScheduleDto createDto)
        {
            var existingSchedule = await _context.Schedules.FirstOrDefaultAsync(s => s.ScheduleDate == createDto.ScheduleDate);
            if (existingSchedule != null)
            {
                throw new InvalidOperationException("Jadwal dengan tanggal dan waktu yang sama sudah ada.");
            }

            var schedule = _mapper.Map<Schedule>(createDto);
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Jadwal baru dibuat dengan ID: {ScheduleId}", schedule.ScheduleId);
            return _mapper.Map<ScheduleDto>(schedule);
        }
        
        // DITAMBAHKAN: Implementasi UpdateAsync
        public async Task<ScheduleDto?> UpdateAsync(int id, UpdateScheduleDto updateDto)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null) return null;

            var existingSchedule = await _context.Schedules.FirstOrDefaultAsync(s => s.ScheduleDate == updateDto.ScheduleDate && s.ScheduleId != id);
            if (existingSchedule != null)
            {
                throw new InvalidOperationException("Jadwal dengan tanggal dan waktu yang sama sudah ada.");
            }

            _mapper.Map(updateDto, schedule);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Jadwal dengan ID: {ScheduleId} telah diperbarui.", id);
            return _mapper.Map<ScheduleDto>(schedule);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null) return false;

            var isInUse = await _context.MenuCourseSchedules.AnyAsync(mcs => mcs.ScheduleId == id);
            if (isInUse)
            {
                throw new InvalidOperationException("Jadwal tidak bisa dihapus karena masih digunakan oleh course.");
            }

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Jadwal dengan ID: {ScheduleId} telah dihapus.", id);
            return true;
        }
    }
}