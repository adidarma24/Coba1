using System.ComponentModel.DataAnnotations;

namespace MyApp.WebAPI.DTOs
{
    public class PaymentMethodCreateDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Logo { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Active";
    }

    public class PaymentMethodUpdateDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Logo { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Active";
    }

    public class PaymentMethodResponseDto
    {
        public int PaymentMethodId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Logo { get; set; }
        public string Status { get; set; } = "Active";
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
