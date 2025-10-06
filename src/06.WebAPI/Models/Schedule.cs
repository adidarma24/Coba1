namespace MyApp.WebAPI.Models
{
  public class Schedule : BaseModel
  {
    public int ScheduleId { get; set; }
    public DateTime ScheduleDate { get; set; }

    public virtual ICollection<MenuCourseSchedule> MenuCourseSchedules { get; set; } = new List<MenuCourseSchedule>();
  }
}
