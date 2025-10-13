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
    private readonly IEmailService _emailService;

    public AuthService(
      UserManager<User> userManager,
      SignInManager<User> signInManager,
      JwtSettings jwtSettings,
      ITokenService tokenService,
      IEmailService emailService)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _jwtSettings = jwtSettings;
      _tokenService = tokenService;
      _emailService = emailService;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
    {
      var existingUser = await _userManager.FindByEmailAsync(request.Email);
      if (existingUser != null)
        throw new ValidationException("Email already registered.");


      if (request.Password != request.ConfirmPassword)
        throw new ValidationException("Passwords do not match.");

      var user = new User
      {
        UserName = request.Email,
        Email = request.Email,
        Name = request.Name,
        Status = UserStatus.Active,
        EmailConfirmed = false
      };

      var result = await _userManager.CreateAsync(user, request.Password);
      if (!result.Succeeded)
      {
        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        throw new ValidationException($"Registration failed: {errors}");
      }

      await _userManager.AddToRoleAsync(user, "User");

      // Generate email confirmation token
      var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
      var encodedToken = Uri.EscapeDataString(token);

      // Create confirmation link (adjust to your frontend domain)
      var confirmLink = $"http://localhost:5099/api/Auth/confirm-email?email={request.Email}&token={encodedToken}";

      // Send confirmation email
      var subject = "Confirm Your Email - Soup";
      var body = $@"
        <p>Hello {user.Name},</p>
        <p>Thank you for registering! Please confirm your email by clicking the link below:</p>
        <p><a href='{confirmLink}' target='_blank'>Confirm Email</a></p>
        <p>If you did not register, please ignore this email.</p>
        <br/>
        <p>Regards,<br/>Soup Team</p>
      ";

      await _emailService.SendEmailAsync(user.Email!, subject, body);

      return new AuthResponseDto
      {
        Success = true,
        Message = "Registration successful! Please check your email to confirm your account."
      };
    }

    public async Task<string> ConfirmEmailAsync(string email, string token)
    {
      var user = await _userManager.FindByEmailAsync(email);
      if (user == null)
        return "http://localhost:5124/email-success?status=notfound";

      var result = await _userManager.ConfirmEmailAsync(user, token);
      if (!result.Succeeded)
      {
        return "http://localhost:5124/email-success?status=failed";
      }

      await _userManager.UpdateAsync(user);

      return "http://localhost:5124/email-success?status=success";
    }

    public async Task<AuthResponseDto> ResendConfirmationEmailAsync(string email)
    {
      var user = await _userManager.FindByEmailAsync(email);
      if (user == null)
        throw new NotFoundException("User not found.");

      if (user.EmailConfirmed)
        throw new ValidationException("Email is already confirmed.");

      var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
      var encodedToken = Uri.EscapeDataString(token);
      var confirmLink = $"http://localhost:5099/api/Auth/confirm-email?email={email}&token={encodedToken}";

      var subject = "Confirm Your Email - Soup";
      var body = $@"
        <p>Hello {user.Name},</p>
        <p>Please confirm your email by clicking the link below:</p>
        <p><a href='{confirmLink}' target='_blank'>Confirm Email</a></p>
        <p>Thank you!</p>
    ";

      await _emailService.SendEmailAsync(user.Email!, subject, body);

      return new AuthResponseDto
      {
        Success = true,
        Message = "Email confirmation send successful."
      };
    }



    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
    {
      var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
      if (user == null)
      {
        return new AuthResponseDto
        {
          Success = false,
          Message = "Invalid email or password"
        };
      }

      if (user.EmailConfirmed == false)
      {
        return new AuthResponseDto
        {
          Success = false,
          Message = "Your email is not confirmed, please check your email or resend email confirmation."
        };
      }

      if (user.Status == UserStatus.Inactive)
      {
        return new AuthResponseDto
        {
          Success = false,
          Message = "Your account is inactive please contact admin at ptsoupco@gmail.com to activate."
        };
      }

      var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
      if (!result.Succeeded)
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

    public async Task<bool> LogoutAsync(string email)
    {
      var user = await _userManager.FindByEmailAsync(email);
      if (user != null)
      {
        user.RefreshToken = null;
        user.ExpiresAt = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);
        return true;
      }

      return false;
    }

    public async Task<AuthResponseDto> SendResetPasswordEmailAsync(ForgotPasswordRequestDto request)
    {
      var user = await _userManager.FindByEmailAsync(request.Email);
      if (user == null)
        throw new NotFoundException("Email not registered.");

      // Generate reset password token
      var token = await _userManager.GeneratePasswordResetTokenAsync(user);
      var encodedToken = Uri.EscapeDataString(token);

      var resetLink = $"http://localhost:5124/new-password?email={request.Email}&token={encodedToken}";

      var subject = "Reset Your Password - Soup";
      var body = $@"
        <p>Hello {user.Name},</p>
        <p>We received a request to reset your password. Click the link below to reset it:</p>
        <p><a href='{resetLink}' target='_blank'>Reset Password</a></p>
        <p>If you did not request this, please ignore this email.</p>
        <br />
        <p>Regards,<br/>Soup Team</p>
      ";

      await _emailService.SendEmailAsync(user.Email!, subject, body);

      return new AuthResponseDto
      {
        Success = true,
        Message = "A password reset link has been sent to your email."
      };
    }

    public async Task<AuthResponseDto> ResetPasswordAsync(ResetPasswordRequestDto request)
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

      return new AuthResponseDto
      {
        Success = true,
        Message = "Password has been reset successfully."
      };
    }
  }
}
