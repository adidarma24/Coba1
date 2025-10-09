namespace WebApplication1.Models
{
    public class Schedule : IAuditable
    {
        public int Id { get; set; }
        public DateTime ScheduleDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation property: Satu jadwal bisa digunakan oleh banyak kursus-jadwal
        public virtual ICollection<MenuCourse_Schedule> MenuCourse_Schedules { get; set; } = new List<MenuCourse_Schedule>();
    }
}