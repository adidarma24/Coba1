using System.ComponentModel.DataAnnotations;

namespace MyApp.WebAPI.Models
{
  public class Schedule : BaseModel
  {
    [Key]
    public int ScheduleId { get; set; } // PK diseragamkan
    public DateTime ScheduleDate { get; set; }

    // Relation
    public ICollection<MenuCourseSchedule> MenuCourseSchedules { get; set; } = new List<MenuCourseSchedule>();
  }
}