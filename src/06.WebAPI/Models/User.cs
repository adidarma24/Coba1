using Microsoft.AspNetCore.Identity;

namespace MyApp.WebAPI.Models
{
  public class User : IdentityUser<int>
  {
    public string Name { get; set; } = string.Empty;
    public UserStatus Status { get; set; } = UserStatus.Active;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Refresh token untuk JWT authentication
    public string? RefreshToken { get; set; }
    public DateTime ExpiresAt { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    public virtual ICollection<MyClass> MyClasses { get; set; } = new List<MyClass>();
  }

  public enum UserStatus
  {
    Active,
    Inactive
  }
}