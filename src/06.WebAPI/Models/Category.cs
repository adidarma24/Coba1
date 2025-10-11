using System.ComponentModel.DataAnnotations;

namespace MyApp.WebAPI.Models
{
  public class Category : BaseModel
  {
    [Key]
    public int CategoryId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Image { get; set; }

    public virtual ICollection<MenuCourse> MenuCourses { get; set; } = new List<MenuCourse>();
  }
}