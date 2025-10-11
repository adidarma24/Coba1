using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.WebAPI.Models
{
    public class MenuCourseSchedule
    {
        [Key]
        public int MSId { get; set; }

        public int Available { get; set; }
        [Required, MaxLength(50)]
        public string Status { get; set; } = "Active"; // Active / InActive

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Foreign Keys
        public int MenuCourseId { get; set; }
        public int ScheduleId { get; set; }

        [ForeignKey(nameof(MenuCourseId))]
        public MenuCourse? MenuCourse { get; set; }

        [ForeignKey(nameof(ScheduleId))]
        public Schedule? Schedule { get; set; }

        // Relations
        public ICollection<InvoiceMenuCourse>? InvoiceMenuCourses { get; set; }
    }
}
