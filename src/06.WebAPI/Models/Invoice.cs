using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.WebAPI.Models
{
  public class Invoice : BaseModel
  {
    [Key]
    public int InvoiceId { get; set; }

    [Required, MaxLength(100)]
    public string NoInvoice { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public int TotalCourse { get; set; }
    public decimal TotalPrice { get; set; }

    // FK
    public int UserIdRef { get; set; }

    [ForeignKey(nameof(UserIdRef))]
    public User? User { get; set; } = default!;

    // Relation
    public virtual ICollection<InvoiceMenuCourse> InvoiceMenuCourses { get; set; } = new List<InvoiceMenuCourse>();
  }
}
