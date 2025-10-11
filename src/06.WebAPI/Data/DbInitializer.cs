using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context)
        {
            try
            {
                await context.Database.MigrateAsync();

                // Hitung PaymentMethod terakhir
                var maxId = await context.PaymentMethods.AnyAsync()
                    ? await context.PaymentMethods.MaxAsync(p => p.PaymentMethodId)
                    : 0;

                await context.Database.ExecuteSqlAsync(
                    $"DBCC CHECKIDENT ('PaymentMethods', RESEED, {maxId})"
                );

                Console.WriteLine($"✅ Database initialized successfully. Max PaymentMethodId: {maxId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Database initialization failed: {ex.Message}");
            }
        }
    }
}
