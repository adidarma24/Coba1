using MyApp.WebAPI.DTOs;

namespace MyApp.WebAPI.Services
{
    public interface IMenuCourseScheduleService
    {
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