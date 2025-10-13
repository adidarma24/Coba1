using Blazored.LocalStorage;
using System.Net.Http.Headers;
using MyApp.BlazorUI.Services.Interfaces;
using MyApp.BlazorUI.DTOs.Auth;
using MyApp.BlazorUI.DTOs;
using System.Text.Json;

namespace MyApp.BlazorUI.Services
{
  public class AuthTokenHandler : DelegatingHandler
  {
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;

    public AuthTokenHandler(HttpClient http, ILocalStorageService localStorage)
    {
      _http = http;
      _localStorage = localStorage;
    }

    public async Task<bool> RefreshTokenAsync()
    {
      string? accessToken = null;
      string? refreshToken = null;

      try
      {
        accessToken = await _localStorage.GetItemAsync<string>("accessToken");
        refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");
      }
      catch (InvalidOperationException)
      {
        // Happens during prerendering when JS interop is not yet available
        return false;
      }

      if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
        return false;

      var dto = new RefreshTokenRequestDto { AccessToken = accessToken, RefreshToken = refreshToken };
      var res = await _http.PostAsJsonAsync("api/Auth/refresh-token", dto);

      if (!res.IsSuccessStatusCode)
        return false;

      var json = await res.Content.ReadFromJsonAsync<ApiResponse<AuthResponseDto>>();
      if (json?.Data?.AccessToken is not string token)
        return false;

      try
      {
        await _localStorage.SetItemAsync("accessToken", token);
        await _localStorage.SetItemAsync("refreshToken", json.Data.RefreshToken);
        await _localStorage.SetItemAsync("expiresAt", json.Data.ExpiresAt);
      }
      catch (InvalidOperationException)
      {

      }

      return true;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      string? token = null;
      try
      {
        token = await _localStorage.GetItemAsync<string>("accessToken");
      }
      catch (InvalidOperationException)
      {

      }

      if (!string.IsNullOrEmpty(token))
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

      var response = await base.SendAsync(request, cancellationToken);

      if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
      {
        // Try to refresh
        var refreshed = await RefreshTokenAsync();
        if (!refreshed) return response;

        try
        {
          var newToken = await _localStorage.GetItemAsync<string>("accessToken");
          if (!string.IsNullOrEmpty(newToken))
          {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
            response = await base.SendAsync(request, cancellationToken);
          }
        }
        catch (InvalidOperationException)
        {

        }
      }

      return response;
    }
  }
}