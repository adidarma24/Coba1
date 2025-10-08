using MyApp.WebAPI.Models;
using MyApp.WebAPI.DTOs.Auth;

namespace MyApp.WebAPI.Services.Interfaces
{
  public interface IAuthService
  {
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
    Task<bool> LogoutAsync(string userEmail);
    Task<bool> ChangePasswordAsync(ChangePasswordRequestDto request);
  }
}
