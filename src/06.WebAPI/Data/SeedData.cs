using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Data
{
  public static class SeedData
  {
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
      using var scope = serviceProvider.CreateScope();
      var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
      var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
      var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

      await context.Database.MigrateAsync();

      // === SEED ROLES ===
      var roles = new[] { "Admin", "User" };
      foreach (var role in roles)
      {
        if (!await roleManager.RoleExistsAsync(role))
          await roleManager.CreateAsync(new IdentityRole<int>(role));
      }

      // === SEED USERS ===
      await SeedUserAsync(userManager, "admin@email.com", "Admin123!", "Admin", "Admin");
      await SeedUserAsync(userManager, "user1@email.com", "User123!", "User", "User 1");
      await SeedUserAsync(userManager, "user2@email.com", "User123!", "User", "User 2");

      // === SEED APP DATA ===
      await SeedAppDataAsync(context);
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
          EmailConfirmed = true,
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow
        };

        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
          await userManager.AddToRoleAsync(user, role);
          Console.WriteLine($"✅ Seeded user: {email} with role {role}");
        }
        else
        {
          Console.WriteLine($"⚠️ Failed to create user {email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
      }
    }

    private static async Task SeedAppDataAsync(ApplicationDbContext context)
    {
      // === SEED CATEGORY ===
      if (!context.Categories.Any())
      {
        context.Categories.AddRange(
            new Category { Name = "Asian", Image = "asian.svg", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { Name = "Western", Image = "western.svg", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        );
        await context.SaveChangesAsync();
      }

      // Ambil ID Category setelah disimpan
      var asianCategory = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Asian");
      var westernCategory = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Western");

      // === SEED MENU COURSE ===
      if (!context.MenuCourses.Any())
      {
        context.MenuCourses.AddRange(
            new MenuCourse
            {
              Name = "Tomyum",
              Image = "tomyum.svg",
              Price = 100000,
              Description = "tomyum",
              CategoryId = asianCategory!.CategoryId,
              CreatedAt = DateTime.UtcNow,
              UpdatedAt = DateTime.UtcNow
            },
            new MenuCourse
            {
              Name = "Pizza",
              Image = "pizza.svg",
              Price = 150000,
              Description = "Learn design principles",
              CategoryId = westernCategory!.CategoryId,
              CreatedAt = DateTime.UtcNow,
              UpdatedAt = DateTime.UtcNow
            }
        );
        await context.SaveChangesAsync();
      }

      // Ambil ID MenuCourse
      var tomyum = await context.MenuCourses.FirstOrDefaultAsync(m => m.Name == "Tomyum");
      var pizza = await context.MenuCourses.FirstOrDefaultAsync(m => m.Name == "Pizza");

      // === SEED SCHEDULE ===
      if (!context.Schedules.Any())
      {
        context.Schedules.AddRange(
            new Schedule
            {
              ScheduleDate = DateTime.UtcNow.AddDays(3),
              CreatedAt = DateTime.UtcNow,
              UpdatedAt = DateTime.UtcNow
            },
            new Schedule
            {
              ScheduleDate = DateTime.UtcNow.AddDays(7),
              CreatedAt = DateTime.UtcNow,
              UpdatedAt = DateTime.UtcNow
            }
        );
        await context.SaveChangesAsync();
      }

      var schedule1 = await context.Schedules.OrderBy(s => s.ScheduleId).FirstAsync();
      var schedule2 = await context.Schedules.OrderBy(s => s.ScheduleId).Skip(1).FirstAsync();

      // === SEED MENU COURSE SCHEDULE ===
      if (!context.MenuCourseSchedules.Any())
      {
        context.MenuCourseSchedules.AddRange(
            new MenuCourseSchedule
            {
              MenuCourseId = tomyum!.MenuCourseId,
              ScheduleId = schedule1.ScheduleId,
              Available = 10,
              Status = "Active",
              CreatedAt = DateTime.UtcNow,
              UpdatedAt = DateTime.UtcNow
            },
            new MenuCourseSchedule
            {
              MenuCourseId = pizza!.MenuCourseId,
              ScheduleId = schedule2.ScheduleId,
              Available = 8,
              Status = "Active",
              CreatedAt = DateTime.UtcNow,
              UpdatedAt = DateTime.UtcNow
            }
        );
        await context.SaveChangesAsync();
      }

      // === SEED PAYMENT METHODS ===
      if (!context.PaymentMethods.Any())
      {
        context.PaymentMethods.AddRange(
            new PaymentMethod { Name = "BCA", Logo = "bca.png", Status = "Active", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new PaymentMethod { Name = "GoPay", Logo = "gopay.png", Status = "Active", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        );
        await context.SaveChangesAsync();
      }
    }
  }
}
