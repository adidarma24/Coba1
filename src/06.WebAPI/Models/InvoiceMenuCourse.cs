using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.WebAPI.Models
{
  public class InvoiceMenuCourse : BaseModel
  {
    [Key]
    public int IMId { get; set; }

    // Foreign Keys
    public int InvoiceId { get; set; }
    public int MSId { get; set; }

    [ForeignKey(nameof(InvoiceId))]
    public Invoice Invoice { get; set; } = null!;

    [ForeignKey(nameof(MSId))]
    public MenuCourseSchedule MenuCourseSchedule { get; set; } = null!;
  }
}
