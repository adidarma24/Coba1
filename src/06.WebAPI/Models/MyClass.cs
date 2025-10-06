namespace MyApp.WebAPI.Models
{
  public class MyClass : BaseModel
  {
    public int MyClassId { get; set; }

    public int UserId { get; set; }
    public int MSId { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual MenuCourseSchedule MenuCourseSchedule { get; set; } = null!;
  }
}