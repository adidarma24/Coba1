namespace MyApp.BlazorUI.Components.Models
{
  public class UserModel
  {
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public UserStatus Status { get; set; }
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