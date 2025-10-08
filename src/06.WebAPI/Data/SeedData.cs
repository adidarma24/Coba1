using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Data
{
  public static class SeedData
  {
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
      using var scope = serviceProvider.CreateScope();
      var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
      var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

      var roles = new[] { "Admin", "User" };
      foreach (var role in roles)
      {
        if (!await roleManager.RoleExistsAsync(role))
        {
          await roleManager.CreateAsync(new IdentityRole<int>(role));
        }
      }

      await SeedUserAsync(userManager, "admin@email.com", "Admin123!", "Admin", "Admin");
      await SeedUserAsync(userManager, "user1@email.com", "User123!", "User", "User 1");
      await SeedUserAsync(userManager, "user2@email.com", "User123!", "User", "User 2");
    }

    private static async Task SeedUserAsync(
        UserManager<User> userManager,
        string email,
        string password,
        string role,
        string name)
    {
      var user = await userManager.FindByEmailAsync(email);
      if (user == null)
      {
        user = new User
        {
          UserName = email,
          Email = email,
          Name = name,
          Status = UserStatus.Active,
          EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
          await userManager.AddToRoleAsync(user, role);
          Console.WriteLine($"Seeded user: {email} with role {role}");
        }
        else
        {
          Console.WriteLine($"Failed to create user {email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
      }
      else
      {
        Console.WriteLine($"ℹUser {email} already exists.");
      }
    }
  }
}
