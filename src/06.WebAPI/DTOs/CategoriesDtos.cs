namespace WebApplication1.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        // Image bisa jadi tidak ada, jadi nullable (?) adalah pilihan yang baik
        public string? Image { get; set; } 
        public int MenuCourseCount { get; set; }
    }

    public class CreateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Image { get; set; }
    }

    public class UpdateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Image { get; set; }
    }
}