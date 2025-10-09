using WebApplication1.DTOs;

namespace WebApplication1.Services
{
    /// <summary>
    /// Interface (kontrak) untuk Schedule Service
    /// Mendefinisikan operasi dasar CRUD untuk data Jadwal (Schedule)
    /// </summary>
    public interface IScheduleService
    {
        /// <summary>
        /// Mengambil semua jadwal yang tersedia
        /// </summary>
        Task<IEnumerable<ScheduleDto>> GetAllAsync();

        /// <summary>
        /// Membuat jadwal baru
        /// </summary>
        Task<ScheduleDto> CreateAsync(CreateScheduleDto createDto);

        /// <summary>
        /// Menghapus jadwal berdasarkan ID
        /// </summary>
        Task<bool> DeleteAsync(int id);
    }
}