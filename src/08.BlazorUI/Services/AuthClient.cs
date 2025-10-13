using Blazored.LocalStorage;
using System.Net.Http.Json;
using MyApp.BlazorUI.Services.Interfaces;
using MyApp.BlazorUI.DTOs.Auth;
using MyApp.BlazorUI.DTOs;
using System.Text.Json;

namespace MyApp.BlazorUI.Services
{

  public class AuthClient : IAuthClient
  {
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;

    public AuthClient(HttpClient http, ILocalStorageService localStorage)
    {
      _http = http;
      _localStorage = localStorage;
    }

    private async Task AddAuthHeaderAsync()
    {
      var token = await _localStorage.GetItemAsync<string>("accessToken");
      if (!string.IsNullOrEmpty(token))
      {
        _http.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
      }
    }

    public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginRequestDto dto)
        => await Post<ApiResponse<AuthResponseDto>>("api/Auth/login", dto);

    public async Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterRequestDto dto)
        => await Post<ApiResponse<AuthResponseDto>>("api/Auth/register", dto);

    public async Task<ApiResponse<AuthResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto dto)
        => await Post<ApiResponse<AuthResponseDto>>("api/Auth/refresh-token", dto);

    public async Task<ApiResponse<bool>> LogoutAsync()
    {
      await AddAuthHeaderAsync();
      return await Post<ApiResponse<bool>>($"api/Auth/logout", new { });
    }

    public async Task<ApiResponse<AuthResponseDto>> ForgotPasswordAsync(ForgotPasswordRequestDto dto)
        => await Post<ApiResponse<AuthResponseDto>>("api/Auth/forgot-password", dto);

    public async Task<ApiResponse<AuthResponseDto>> ResetPasswordAsync(ResetPasswordRequestDto dto)
        => await Post<ApiResponse<AuthResponseDto>>("api/Auth/reset-password", dto);

    public async Task<ApiResponse<AuthResponseDto>> ResendConfirmationEmailAsync(string email)
      => await Post<ApiResponse<AuthResponseDto>>("api/Auth/resend-confirmation-email", email);

    // private async Task<T?> Post<T>(string url, object? body)
    // {
    //   var response = await _http.PostAsJsonAsync(url, body);

    //   if (!response.IsSuccessStatusCode)
    //   {
    //     var error = await response.Content.ReadAsStringAsync();
    //     Console.WriteLine($"Server returned error ({response.StatusCode}): {error}");
    //     return default;
    //   }

    //   var content = await response.Content.ReadAsStringAsync();
    //   if (string.IsNullOrWhiteSpace(content))
    //   {
    //     Console.WriteLine("Response content is empty.");
    //     return default;
    //   }

    //   return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
    //   {
    //     PropertyNameCaseInsensitive = true
    //   });
    // }

    private async Task<T?> Post<T>(string url, object? body)
    {
      HttpResponseMessage response;

      // If body is null, send an empty request instead of JSON "null"
      if (body is null)
        response = await _http.PostAsync(url, null);
      else
        response = await _http.PostAsJsonAsync(url, body);

      // Try to read a valid JSON response
      try
      {
        var content = await response.Content.ReadAsStringAsync();

        // Try to deserialize your API's ApiResponse<T> type even when it's an error
        var result = System.Text.Json.JsonSerializer.Deserialize<T>(content,
            new System.Text.Json.JsonSerializerOptions
            {
              PropertyNameCaseInsensitive = true
            });

        // if the request failed (e.g., 400/401/etc), still return deserialized error response
        if (!response.IsSuccessStatusCode)
        {
          Console.WriteLine($"Server returned error ({response.StatusCode}): {content}");
        }

        return result;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Deserialization error: {ex.Message}");
        return default;
      }
    }


  }

}