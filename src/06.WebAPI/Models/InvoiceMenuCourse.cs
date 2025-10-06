namespace MyApp.WebAPI.Models
{
  public class InvoiceMenuCourse : BaseModel
  {
    public int IMId { get; set; }

    public int InvoiceId { get; set; }
    public int MSId { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
    public virtual MenuCourseSchedule MenuCourseSchedule { get; set; } = null!;
  }
}
