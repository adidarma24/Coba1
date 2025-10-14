namespace MyApp.WebAPI.Models
{
  public class MenuCourse : BaseModel
  {
    public int MenuCourseId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Image { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }

    // Foreign Key
    public int CategoryId { get; set; }

    // Navigation Properties
    public virtual Category Category { get; set; } = null!;
    public virtual ICollection<MenuCourseSchedule> MenuCourseSchedules { get; set; } = new List<MenuCourseSchedule>();
  }
}