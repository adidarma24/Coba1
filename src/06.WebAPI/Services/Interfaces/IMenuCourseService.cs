using MyApp.WebAPI.DTOs;

namespace MyApp.WebAPI.Services
{
    public interface IMenuCourseService
    {
        Task<IEnumerable<MenuCourseDto>> GetAllMenuCoursesAsync();
        Task<MenuCourseDto?> GetMenuCourseByIdAsync(int id);
        Task<MenuCourseDto> CreateMenuCourseAsync(CreateMenuCourseDto createDto);
        Task<MenuCourseDto?> UpdateMenuCourseAsync(int id, UpdateMenuCourseDto updateDto);
        Task<bool> DeleteMenuCourseAsync(int id);
    }
}