namespace MyApp.WebAPI.Models
{
    public class Schedule : IAuditable
    {
        public int Id { get; set; } // PK diseragamkan
        public DateTime ScheduleDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Relation
        public ICollection<MenuCourseSchedule> MenuCourseSchedules { get; set; } = new List<MenuCourseSchedule>();
    }
}