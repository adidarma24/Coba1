namespace MyApp.WebAPI.Models
{
  public class Invoice : BaseModel
  {
    public int InvoiceId { get; set; }
    public string NoInvoice { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int TotalCourse { get; set; }
    public decimal TotalPrice { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual ICollection<InvoiceMenuCourse> InvoiceMenuCourses { get; set; } = new List<InvoiceMenuCourse>();
  }
}
