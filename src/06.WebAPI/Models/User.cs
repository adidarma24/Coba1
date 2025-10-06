namespace MyApp.WebAPI.Models
{
  public class User : BaseModel
  {
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.User;
    public UserStatus Status { get; set; } = UserStatus.Active;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    public virtual ICollection<MyClass> MyClasses { get; set; } = new List<MyClass>();

  }

  public enum UserRole
  {
    Admin,
    User
  }

  public enum UserStatus
  {
    Active,
    Inactive
  }
}