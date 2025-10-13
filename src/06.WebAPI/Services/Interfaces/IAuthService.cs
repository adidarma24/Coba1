using MyApp.WebAPI.Models;
using MyApp.WebAPI.DTOs.Auth;

namespace MyApp.WebAPI.Services.Interfaces
{
  public interface IAuthService
  {
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
    Task<string> ConfirmEmailAsync(string email, string token);
    Task<AuthResponseDto> ResendConfirmationEmailAsync(string email);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
    Task<bool> LogoutAsync(string email);
    Task<AuthResponseDto> SendResetPasswordEmailAsync(ForgotPasswordRequestDto request);
    Task<AuthResponseDto> ResetPasswordAsync(ResetPasswordRequestDto request);
  }
}
