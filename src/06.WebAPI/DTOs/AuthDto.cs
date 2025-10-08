using System.ComponentModel.DataAnnotations;

namespace MyApp.WebAPI.DTOs.Auth
{
  public class RegisterRequestDto
  {
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).{6,}$",
    ErrorMessage = "Password must be at least 6 characters, contain at least one uppercase letter and contain at least one number!")]
    public string Password { get; set; } = string.Empty;
  }

  public class LoginRequestDto
  {
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
  }

  public class RefreshTokenRequestDto
  {
    [Required]
    public string AccessToken { get; set; } = string.Empty;

    [Required]
    public string RefreshToken { get; set; } = string.Empty;
  }

  public class ChangePasswordRequestDto
  {
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).{6,}$",
    ErrorMessage = "Password must be at least 6 characters, contain at least one uppercase letter and contain at least one number!")]
    public string NewPassword { get; set; } = string.Empty;
  }

  public class AuthResponseDto
  {
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string? RefreshToken { get; set; }
    public DateTime ExpiresAt { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
  }
}
