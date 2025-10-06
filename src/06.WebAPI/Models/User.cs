using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.WebAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(255)]
        public string Password { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Row { get; set; } = "User"; // Admin / User

        [Required, MaxLength(50)]
        public string Status { get; set; } = "Active"; // Active / Inactive

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Relationships
        public ICollection<Invoice>? Invoices { get; set; } = new List<Invoice>();
        public ICollection<MyClass>? MyClasses { get; set; } = new List<MyClass>();
    }
}
