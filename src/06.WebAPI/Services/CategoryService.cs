using AutoMapper;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.DTOs;
using MyApp.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MyApp.WebAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ApplicationDbContext context, IMapper mapper, ILogger<CategoryService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories
                .Include(c => c.MenuCourses)
                .ToListAsync();
                
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.MenuCourses)
                .FirstOrDefaultAsync(c => c.Id == id);

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);

            
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Category created: {CategoryName} with ID: {CategoryId}", 
                category.Name, category.Id);

            var categoryDto = _mapper.Map<CategoryDto>(category);
            categoryDto.MenuCourseCount = 0; // Set manual karena relasi belum di-load

            return categoryDto;
        }

        public async Task<CategoryDto?> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
          
            var category = await _context.Categories
                .Include(c => c.MenuCourses)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return null;
            }

            _mapper.Map(updateCategoryDto, category);
            
          

            await _context.SaveChangesAsync();

            _logger.LogInformation("Category updated: {CategoryId}", id);

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return false;
            }
            
            
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Category deleted: {CategoryId}", id);

            return true;
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }
    }
}