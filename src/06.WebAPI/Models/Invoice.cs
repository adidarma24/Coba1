using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.WebAPI.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }

        [Required, MaxLength(100)]
        public string NoInvoice { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public int TotalCourse { get; set; }
        public double TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // FK
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        // Relation
        public ICollection<InvoiceMenuCourse>? InvoiceMenuCourses { get; set; }
    }
}
