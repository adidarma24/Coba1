// Lokasi: src/06.WebAPI/Services/IScheduleService.cs

using MyApp.WebAPI.DTOs;

namespace MyApp.WebAPI.Services
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleDto>> GetAllAsync();
        
        // Pastikan metode ini ada
        Task<ScheduleDto?> GetAsync(int id); 

        Task<ScheduleDto> CreateAsync(CreateScheduleDto createDto);
        
        Task<ScheduleDto?> UpdateAsync(int id, UpdateScheduleDto updateDto);

        Task<bool> DeleteAsync(int id);
    }
}