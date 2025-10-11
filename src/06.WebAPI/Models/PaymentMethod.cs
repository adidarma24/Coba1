using System.ComponentModel.DataAnnotations;

namespace MyApp.WebAPI.Models
{
  public class PaymentMethod
  {
    [Key]
    public int PaymentMethodId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Logo { get; set; }

    [MaxLength(50)]
    public string Status { get; set; } = "Active";

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
  }
}
