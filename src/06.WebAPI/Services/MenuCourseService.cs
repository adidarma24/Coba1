using AutoMapper;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.DTOs;
using MyApp.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MyApp.WebAPI.Services
{
  public class MenuCourseService : IMenuCourseService
  {
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<MenuCourseService> _logger;

    public MenuCourseService(ApplicationDbContext context, IMapper mapper, ILogger<MenuCourseService> logger)
    {
      _context = context;
      _mapper = mapper;
      _logger = logger;
    }

    public async Task<IEnumerable<MenuCourseDto>> GetAllMenuCoursesAsync()
    {
      var menuCourses = await _context.MenuCourses
          .Include(mc => mc.Category)
          .ToListAsync();
      return _mapper.Map<IEnumerable<MenuCourseDto>>(menuCourses);
    }

    public async Task<MenuCourseDto?> GetMenuCourseByIdAsync(int id)
    {
      var menuCourse = await _context.MenuCourses
          .Include(mc => mc.Category)
          .FirstOrDefaultAsync(mc => mc.MenuCourseId == id);

      return _mapper.Map<MenuCourseDto>(menuCourse);
    }

    public async Task<MenuCourseDto> CreateMenuCourseAsync(CreateMenuCourseDto createDto)
    {
      var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == createDto.CategoryId);
      if (!categoryExists)
      {
        throw new ArgumentException($"Category dengan ID {createDto.CategoryId} tidak ditemukan.");
      }

      var menuCourse = _mapper.Map<MenuCourse>(createDto);

      _context.MenuCourses.Add(menuCourse);
      await _context.SaveChangesAsync();

      await _context.Entry(menuCourse).Reference(mc => mc.Category).LoadAsync();

      _logger.LogInformation("MenuCourse created with ID: {MenuCourseId}", menuCourse.MenuCourseId);
      return _mapper.Map<MenuCourseDto>(menuCourse);
    }

    public async Task<MenuCourseDto?> UpdateMenuCourseAsync(int id, UpdateMenuCourseDto updateDto)
    {
      var menuCourse = await _context.MenuCourses.FindAsync(id);
      if (menuCourse == null) return null;

      if (menuCourse.CategoryId != updateDto.CategoryId)
      {
        var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == updateDto.CategoryId);
        if (!categoryExists)
        {
          throw new ArgumentException($"Category dengan ID {updateDto.CategoryId} tidak ditemukan.");
        }
      }

      _mapper.Map(updateDto, menuCourse);

      await _context.SaveChangesAsync();

      if (menuCourse.CategoryId != updateDto.CategoryId)
      {
        await _context.Entry(menuCourse).Reference(mc => mc.Category).LoadAsync();
      }

      _logger.LogInformation("MenuCourse updated with ID: {MenuCourseId}", menuCourse.MenuCourseId);
      return _mapper.Map<MenuCourseDto>(menuCourse);
    }

    public async Task<bool> DeleteMenuCourseAsync(int id)
    {
      var menuCourse = await _context.MenuCourses.FindAsync(id);
      if (menuCourse == null) return false;

      _context.MenuCourses.Remove(menuCourse);
      await _context.SaveChangesAsync();

      // DIPERBAIKI: Menggunakan 'Id' yang sudah distandarkan
      _logger.LogInformation("MenuCourse deleted with ID: {MenuCourseId}", menuCourse.MenuCourseId);
      return true;
    }
  }
}