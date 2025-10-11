using System.ComponentModel.DataAnnotations;

namespace MyApp.WebAPI.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }

        public DateTime ScheduleDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Relation
        public ICollection<MenuCourseSchedule>? MenuCourseSchedules { get; set; }
    }
}
