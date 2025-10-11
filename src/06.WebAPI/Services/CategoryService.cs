// Import AutoMapper untuk object-to-object mapping
using AutoMapper;
// Import DbContext untuk database operations
using WebApplication1.Data;
// Import DTOs untuk data transfer objects
using WebApplication1.DTOs;
// Import Models untuk entities
using WebApplication1.Models;
// Import Entity Framework Core untuk database operations
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services
{
    public class CategoryService : ICategoryService
    {
        // DIPERBAIKI: Menggunakan ApplicationDbContext yang sudah di-rename
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
            // DIOPTIMALKAN: Tambahkan .Include() untuk memuat data MenuCourses
            // agar AutoMapper bisa menghitung MenuCourseCount dengan benar.
            var categories = await _context.Categories
                .Include(c => c.MenuCourses)
                .ToListAsync();
                
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            // DIOPTIMALKAN: Ganti FindAsync ke FirstOrDefaultAsync agar bisa menggunakan .Include()
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

            // Kita perlu memuat ulang data untuk mendapatkan MenuCourseCount yang akurat (yaitu 0)
            // Namun untuk Create, kita bisa langsung map karena kita tahu count-nya 0
            var categoryDto = _mapper.Map<CategoryDto>(category);
            categoryDto.MenuCourseCount = 0; // Set manual karena relasi belum di-load

            return categoryDto;
        }

        public async Task<CategoryDto?> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            // DIOPTIMALKAN: Gunakan .Include() agar saat update,
            // kita bisa mengembalikan DTO dengan MenuCourseCount yang benar.
            var category = await _context.Categories
                .Include(c => c.MenuCourses)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return null;
            }

            _mapper.Map(updateCategoryDto, category);
            
            // UpdatedAt di-handle otomatis oleh SaveChangesAsync di DbContext (via IAuditable).
            // Komentar yang tidak perlu dihapus.

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
            
            // DbContext akan melempar exception jika ada MenuCourse yang terkait,
            // karena kita set OnDelete(DeleteBehavior.Restrict).
            // Penanganan error ini sebaiknya dilakukan di Controller.
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