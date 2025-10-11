using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.WebAPI.Models
{
    public class InvoiceMenuCourse
    {
        [Key]
        public int IMId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Foreign Keys
        public int InvoiceId { get; set; }
        public int MSId { get; set; }

        [ForeignKey(nameof(InvoiceId))]
        public Invoice? Invoice { get; set; }

        [ForeignKey(nameof(MSId))]
        public MenuCourseSchedule? MenuCourseSchedule { get; set; }
    }
}
