using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.WebAPI.Models
{
  public class MyClass : BaseModel
  {
    [Key]
    public int MyClassId { get; set; }

    // Foreign Keys
    public int UserIdRef { get; set; }
    public int MSId { get; set; }

    [ForeignKey(nameof(UserIdRef))]
    public User? User { get; set; } = default!;

    [ForeignKey(nameof(MSId))]
    public MenuCourseSchedule? MenuCourseSchedule { get; set; } = default!;
  }
}
