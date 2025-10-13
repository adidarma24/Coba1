using MyApp.BlazorUI.DTOs;
using MyApp.BlazorUI.DTOs.Auth;

namespace MyApp.BlazorUI.Services.Interfaces
{
  public interface IAuthService
  {
    Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginRequestDto dto);
    Task<ApiResponse<bool>> LogoutAsync();
    Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterRequestDto dto);
    Task<ApiResponse<AuthResponseDto>> ResendConfirmationEmailAsync(string email);
    Task<ApiResponse<AuthResponseDto>> RefreshTokenAsync();
    Task<bool> IsUserAuthenticatedAsync();
    Task<ApiResponse<AuthResponseDto>> ForgotPasswordAsync(ForgotPasswordRequestDto dto);
    Task<ApiResponse<AuthResponseDto>> ResetPasswordAsync(ResetPasswordRequestDto dto);
  }

}