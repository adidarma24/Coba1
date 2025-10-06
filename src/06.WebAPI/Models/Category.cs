using System.ComponentModel.DataAnnotations;

namespace MyApp.WebAPI.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Image { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Relation
        public ICollection<MenuCourse>? MenuCourses { get; set; }
    }
}
