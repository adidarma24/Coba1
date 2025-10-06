namespace MyApp.WebAPI.DTOs
{
  // to get all invoice
  public class InvoiceDTO
  {
    public int InvoiceId { get; set; }
    public string NoInvoice { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int TotalCourse { get; set; }
    public decimal TotalPrice { get; set; }
  }

  // to get detail invoice
  public class DetailInvoiceDTO
  {
    public int InvoiceId { get; set; }
    public string NoInvoice { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal TotalPrice { get; set; }
    public List<CourseItemDTO> ListCourse { get; set; } = new List<CourseItemDTO>();

  }

  public class CourseItemDTO
  {
    public int MenuCourseId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public DateTime ScheduleDate { get; set; }
    public decimal Price { get; set; }
  }

  public class CreateInvoiceDTO
  {
    public int UserId { get; set; }
    public List<int> MSId { get; set; } = new List<int>();
  }
}