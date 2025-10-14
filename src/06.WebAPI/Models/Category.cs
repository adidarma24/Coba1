namespace MyApp.WebAPI.Models
{
    public class Category : IAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<MenuCourse> MenuCourses { get; set; } = new List<MenuCourse>();
    }
}