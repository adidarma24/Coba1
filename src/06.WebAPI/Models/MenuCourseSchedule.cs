using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.WebAPI.Models
{
  public class MenuCourseSchedule : BaseModel
  {
    [Key]
    public int MSId { get; set; }

    public int AvailableSlot { get; set; }
    [Required, MaxLength(50)]
    public MSStatus Status { get; set; } = MSStatus.Active;

    // Foreign Keys
    public int MenuCourseId { get; set; }
    public int ScheduleId { get; set; }

    [ForeignKey(nameof(MenuCourseId))]
    public MenuCourse MenuCourse { get; set; } = null!;

    [ForeignKey(nameof(ScheduleId))]
    public Schedule Schedule { get; set; } = null!;

    // Relations
    public virtual ICollection<InvoiceMenuCourse> InvoiceMenuCourses { get; set; } = new List<InvoiceMenuCourse>();
    public virtual ICollection<MyClass> MyClasses { get; set; } = new List<MyClass>();
  }

  public enum MSStatus
  {
    Active,
    Inactive
  }
}
