using MyApp.BlazorUI.DTOs;
using MyApp.BlazorUI.DTOs.Auth;

namespace MyApp.BlazorUI.Services.Interfaces
{
  public interface IAuthClient
  {
    Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginRequestDto dto);
    Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterRequestDto dto);
    Task<ApiResponse<AuthResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto dto);
    Task<ApiResponse<bool>> LogoutAsync();
    Task<ApiResponse<AuthResponseDto>> ForgotPasswordAsync(ForgotPasswordRequestDto dto);
    Task<ApiResponse<AuthResponseDto>> ResetPasswordAsync(ResetPasswordRequestDto dto);
    Task<ApiResponse<AuthResponseDto>> ResendConfirmationEmailAsync(string email);
  }
}