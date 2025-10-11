using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.WebAPI.Models
{
    public class MenuCourse
    {
        [Key]
        public int MenuCourseId { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Image { get; set; }

        public double Price { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // FK
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }

        // Relation
        public ICollection<MenuCourseSchedule>? MenuCourseSchedules { get; set; }
        public ICollection<MyClass>? MyClasses { get; set; }
    }
}
