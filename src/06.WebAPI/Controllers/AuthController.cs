using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.DTOs.Auth;
using MyApp.WebAPI.Services.Interfaces;
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
      _authService = authService;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Register([FromBody] RegisterRequestDto dto)
    {
      var result = await _authService.RegisterAsync(dto);
      if (result.Success)
      {
        return Ok(new ApiResponse<AuthResponseDto>
        {
          Success = true,
          Data = result,
          Message = "Registration successful"
        });
      }

      return BadRequest(new ApiResponse<AuthResponseDto>
      {
        Success = false,
        Data = result,
        Message = result.Message
      });
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login([FromBody] LoginRequestDto dto)
    {
      var result = await _authService.LoginAsync(dto);
      if (result.Success)
      {
        return Ok(new ApiResponse<AuthResponseDto>
        {
          Success = true,
          Data = result,
          Message = "Login successful"
        });
      }

      return Unauthorized(new ApiResponse<AuthResponseDto>
      {
        Success = false,
        Data = result,
        Message = result.Message
      });
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Refresh([FromBody] RefreshTokenRequestDto dto)
    {
      var result = await _authService.RefreshTokenAsync(dto);

      if (result.Success)
      {
        return Ok(new ApiResponse<AuthResponseDto>
        {
          Success = true,
          Data = result,
          Message = "Token refreshed successfully"
        });
      }

      return Unauthorized(new ApiResponse<AuthResponseDto>
      {
        Success = false,
        Data = result,
        Message = result.Message
      });

    }

    [HttpPost("logout")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<bool>>> Logout([FromQuery] string email)
    {
      var result = await _authService.LogoutAsync(email);
      return Ok(new ApiResponse<bool>
      {
        Success = result,
        Data = result,
        Message = result ? "Logout successful" : "Logout failed"
      });
    }

    [HttpPost("forgot-password")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<bool>>> ForgotPassword([FromBody] ForgotPasswordRequestDto dto)
    {
      var result = await _authService.SendResetPasswordEmailAsync(dto);
      return Ok(new ApiResponse<bool>
      {
        Success = result,
        Data = result,
        Message = result ? "Password reset email sent successfully." : "Failed to sent email"
      });
    }

    [HttpPost("reset-password")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<bool>>> ResetPassword([FromBody] ResetPasswordRequestDto request)
    {
      var result = await _authService.ResetPasswordAsync(request);
      return Ok(new ApiResponse<bool>
      {
        Success = result,
        Data = result,
        Message = result ? "Password has been reset successfully." : "Failed to reset password"
      });
    }
  }
}
