using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyApp.WebAPI.Services.Interfaces;
using MyApp.WebAPI.Configuration;
using MyApp.WebAPI.Exceptions;
using MyApp.WebAPI.DTOs.Auth;
using MyApp.WebAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Security.Claims;
using System.Text;

namespace MyApp.WebAPI.Services
{
  public class AuthService : IAuthService
  {
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly JwtSettings _jwtSettings;
    private readonly ITokenService _tokenService;

    public AuthService(
      UserManager<User> userManager,
      SignInManager<User> signInManager,
      JwtSettings jwtSettings,
      ITokenService tokenService)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _jwtSettings = jwtSettings;
      _tokenService = tokenService;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
    {
      var existingUser = await _userManager.FindByEmailAsync(request.Email);
      if (existingUser != null)
      {
        return new AuthResponseDto
        {
          Success = false,
          Message = "Email already registered."
        };
      }

      if (request.Password != request.ConfirmPassword)
        throw new ValidationException("Passwords do not match.");

      var user = new User
      {
        UserName = request.Email,
        Email = request.Email,
        Name = request.Name,
        Status = UserStatus.Active,
        EmailConfirmed = true
      };

      var result = await _userManager.CreateAsync(user, request.Password);
      if (!result.Succeeded)
      {
        var errors = string.Join(", ", result.Errors.Select(e => e.Description));

        return new AuthResponseDto
        {
          Success = false,
          Message = $"Registration failed: {errors}"
        };
      }

      await _userManager.AddToRoleAsync(user, "User");

      var accessToken = await _tokenService.CreateTokenAsync(user);
      var refreshToken = _tokenService.GenerateRefreshToken();

      // Save refresh token ke database
      user.RefreshToken = refreshToken;
      user.ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
      await _userManager.UpdateAsync(user);

      return new AuthResponseDto
      {
        Success = true,
        Message = "Registration successful",
        AccessToken = accessToken,
        RefreshToken = refreshToken,
        ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
        Email = user.Email!,
        Name = user.Name,
        Role = "User"
      };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
    {
      var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
      if (user == null || user.Status == UserStatus.Inactive)
      {
        return new AuthResponseDto
        {
          Success = false,
          Message = "Invalid email or password"
        };
      }

      var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
      if (result == null)
      {
        return new AuthResponseDto
        {
          Success = false,
          Message = "Invalid email or password"
        };
      }

      var accessToken = await _tokenService.CreateTokenAsync(user);
      var refreshToken = _tokenService.GenerateRefreshToken();

      // Save refresh token
      user.RefreshToken = refreshToken;
      user.ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
      await _userManager.UpdateAsync(user);

      var roles = await _userManager.GetRolesAsync(user);

      return new AuthResponseDto
      {
        Success = true,
        Message = "Login successful",
        AccessToken = accessToken,
        RefreshToken = refreshToken,
        ExpiresAt = user.ExpiresAt,
        Email = user.Email!,
        Name = user.Name,
        Role = roles.FirstOrDefault() ?? "User"
      };
    }

    public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request)
    {
      // Extract claims dari expired access token
      var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
      if (principal == null)
      {
        return new AuthResponseDto
        {
          Success = false,
          Message = "Invalid access token"
        };
      }

      var userIdClaim = principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
      if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
      {
        return new AuthResponseDto
        {
          Success = false,
          Message = "Invalid token claims"
        };
      }

      // Validasi refresh token
      var user = await _userManager.FindByIdAsync(userId.ToString());
      if (user == null || user.Status == UserStatus.Inactive || user.RefreshToken != request.RefreshToken ||
          user.ExpiresAt <= DateTime.UtcNow)
      {
        return new AuthResponseDto
        {
          Success = false,
          Message = "Invalid refresh token"
        };
      }

      // Generate tokens baru
      var accessToken = await _tokenService.CreateTokenAsync(user);
      var refreshToken = _tokenService.GenerateRefreshToken();

      // Update refresh token
      user.RefreshToken = refreshToken;
      user.ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
      await _userManager.UpdateAsync(user);

      var roles = await _userManager.GetRolesAsync(user);

      return new AuthResponseDto
      {
        Success = true,
        Message = "Token refreshed successfully",
        AccessToken = accessToken,
        RefreshToken = refreshToken,
        ExpiresAt = user.ExpiresAt,
        Email = user.Email!,
        Name = user.Name,
        Role = roles.FirstOrDefault() ?? "User"
      };
    }

    public async Task<bool> LogoutAsync(string userEmail)
    {
      var user = await _userManager.FindByEmailAsync(userEmail);
      if (user != null)
      {
        // Invalidate refresh token
        user.RefreshToken = null;
        user.ExpiresAt = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);

        return true;
      }

      return false;
    }

    public async Task<bool> SendResetPasswordEmailAsync(ForgotPasswordRequestDto request)
    {
      var user = await _userManager.FindByEmailAsync(request.Email);
      if (user == null)
        throw new NotFoundException("Email not registered.");

      // Generate reset password token
      var token = await _userManager.GeneratePasswordResetTokenAsync(user);

      // Normally you'd send this via email
      // For now, log it or return it for testing
      // TODO: Use IEmailService to send email with token
      Console.WriteLine($"Reset password token for {request.Email}: {token}");

      return true;
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordRequestDto request)
    {
      var user = await _userManager.FindByEmailAsync(request.Email);
      if (user == null)
        throw new NotFoundException("Email not registered.");

      var isSamePassword = await _userManager.CheckPasswordAsync(user, request.NewPassword);
      if (isSamePassword)
        throw new ValidationException("New password cannot be the same as your old password.");

      if (request.NewPassword != request.ConfirmNewPassword)
        throw new ValidationException("Passwords do not match.");

      var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
      if (!result.Succeeded)
      {
        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        throw new ValidationException("RESET_FAILED", errors);
      }

      user.RefreshToken = null;
      user.ExpiresAt = DateTime.UtcNow;
      await _userManager.UpdateAsync(user);

      return true;
    }
  }
}
