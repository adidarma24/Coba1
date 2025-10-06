namespace MyApp.WebAPI.Models
{
  public class Category : BaseModel
  {
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;

    public virtual ICollection<MenuCourse> MenuCourses { get; set; } = new List<MenuCourse>();
  }
}