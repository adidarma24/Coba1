namespace MyApp.WebAPI.Models
{
    public class MenuCourse_Schedule : IAuditable
    {
        public int Id { get; set; }
        public int AvailableSlot { get; set; }
        public string Status { get; set; } = "Active"; // Misal: "Active", "Inactive", "Full"

        // Foreign Keys
        public int MenuCourseId { get; set; }
        public int ScheduleId { get; set; }

        // Timestamps
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Properties
        public virtual MenuCourse MenuCourse { get; set; } = null!;
        public virtual Schedule Schedule { get; set; } = null!;
    }
}