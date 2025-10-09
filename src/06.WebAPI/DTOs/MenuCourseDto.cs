// DTOs/MenuCourseDto.cs
namespace WebApplication1.DTOs
{
    /// <summary>
    /// DTO untuk menampilkan data MenuCourse
    /// </summary>
    public class MenuCourseDto
    {
        public int Id { get; set; } // DIPERBAIKI: Diseragamkan menjadi 'Id'
        public string Name { get; set; } = string.Empty; // DITAMBAHKAN: Nilai awal untuk hilangkan warning
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty; // DITAMBAHKAN: Nilai awal untuk hilangkan warning
    }

    /// <summary>
    /// DTO untuk membuat MenuCourse baru
    /// </summary>
    public class CreateMenuCourseDto
    {
        public string Name { get; set; } = string.Empty; // DITAMBAHKAN: Nilai awal
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
    }

    /// <summary>
    /// DTO untuk memperbarui MenuCourse
    /// </summary>
    public class UpdateMenuCourseDto
    {
        public string Name { get; set; } = string.Empty; // DITAMBAHKAN: Nilai awal
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
    }
}