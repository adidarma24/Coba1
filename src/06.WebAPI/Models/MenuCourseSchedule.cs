namespace MyApp.WebAPI.Models
{
    public class MenuCourseSchedule : IAuditable
    {
        public int MSId { get; set; } // PK diseragamkan
        public int AvailableSlot { get; set; }
        public MSStatus Status { get; set; } = MSStatus.Active;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Foreign Keys
        public int MenuCourseId { get; set; }
        public int ScheduleId { get; set; }

        // Navigation Properties
        public virtual MenuCourse MenuCourse { get; set; } = null!;
        public virtual Schedule Schedule { get; set; } = null!;
        public virtual ICollection<InvoiceMenuCourse> InvoiceMenuCourses { get; set; } = new List<InvoiceMenuCourse>();
        public virtual ICollection<MyClass> MyClasses { get; set; } = new List<MyClass>();
    }

    public enum MSStatus
    {
        Active,
        Inactive
    }
}