using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.WebAPI.Models
{
  public class MenuCourseSchedule : BaseModel
  {
    [Key]
    public int MSId { get; set; } // PK diseragamkan
    public int AvailableSlot { get; set; }
    [Required, MaxLength(50)]
    public MSStatus Status { get; set; } = MSStatus.Active;

    // Foreign Keys
    public int MenuCourseId { get; set; }
    public int ScheduleId { get; set; }

    // Navigation Properties
    [ForeignKey(nameof(MenuCourseId))]
    public virtual MenuCourse MenuCourse { get; set; } = null!;

    [ForeignKey(nameof(ScheduleId))]
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