using AutoMapper;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services
{
    /// <summary>
    /// Implementasi logika bisnis untuk operasi Jadwal (Schedule)
    /// </summary>
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

        public async Task<ScheduleDto> CreateAsync(CreateScheduleDto createDto)
        {
            // Validasi: Cegah pembuatan jadwal dengan tanggal & waktu yang sama persis
            var existingSchedule = await _context.Schedules.FirstOrDefaultAsync(s => s.ScheduleDate == createDto.ScheduleDate);
            if (existingSchedule != null)
            {
                throw new ArgumentException("Jadwal dengan tanggal dan waktu yang sama sudah ada.");
            }

            var schedule = _mapper.Map<Schedule>(createDto);
            
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Jadwal baru dibuat dengan ID: {ScheduleId}", schedule.Id);
            return _mapper.Map<ScheduleDto>(schedule);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return false; // Tidak ditemukan
            }

            // Validasi: Jangan hapus jadwal jika masih digunakan oleh sebuah course
            var isInUse = await _context.MenuCourse_Schedules.AnyAsync(mcs => mcs.ScheduleId == id);
            if (isInUse)
            {
                // Anda bisa melempar exception atau hanya mengembalikan false
                // Melempar exception lebih informatif untuk controller
                throw new InvalidOperationException("Jadwal tidak bisa dihapus karena masih digunakan oleh course.");
            }

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Jadwal dengan ID: {ScheduleId} telah dihapus.", id);
            return true;
        }
    }
}