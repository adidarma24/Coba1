using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.WebAPI.Models
{
  public class MenuCourse : BaseModel
  {
    [Key]
    public int MenuCourseId { get; set; }

    [Required, MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Image { get; set; }

    public decimal Price { get; set; }

    public string? Description { get; set; }

    // FK
    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; } = null!;

    // Relation
    public virtual ICollection<MenuCourseSchedule> MenuCourseSchedules { get; set; } = new List<MenuCourseSchedule>();
  }
}
