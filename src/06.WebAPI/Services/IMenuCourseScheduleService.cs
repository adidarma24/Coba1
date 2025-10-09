using WebApplication1.DTOs;

namespace WebApplication1.Services
{
    /// <summary>
    /// Interface (kontrak) untuk layanan yang menghubungkan MenuCourse dengan Schedule
    /// </summary>
    public interface IMenuCourseScheduleService
    {
        /// <summary>
        /// Mendapatkan semua jadwal yang terdaftar untuk sebuah course tertentu
        /// </summary>
        Task<IEnumerable<MenuCourseScheduleDto>> GetSchedulesForCourseAsync(int menuCourseId);
        
        /// <summary>
        /// Menambahkan/mendaftarkan sebuah jadwal ke sebuah course
        /// </summary>
        Task<MenuCourseScheduleDto> AssignScheduleAsync(CreateMenuCourseScheduleDto createDto);
        
        /// <summary>
        /// Memperbarui detail pendaftaran jadwal (misal: slot atau status)
        /// </summary>
        Task<MenuCourseScheduleDto?> UpdateAssignmentAsync(int id, UpdateMenuCourseScheduleDto updateDto);
        
        /// <summary>
        /// Menghapus/membatalkan pendaftaran jadwal dari sebuah course
        /// </summary>
        Task<bool> UnassignScheduleAsync(int id);
    }
}