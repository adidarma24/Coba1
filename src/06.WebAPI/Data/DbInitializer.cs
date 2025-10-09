using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            try
            {
                // Pastikan database sudah ada
                context.Database.EnsureCreated();

                // 🔄 Reset identity PaymentMethods agar ID berurutan
                var maxId = context.PaymentMethods.Any() ? context.PaymentMethods.Max(p => p.PaymentMethodId) : 0;
                context.Database.ExecuteSqlRaw($"DBCC CHECKIDENT ('PaymentMethods', RESEED, {maxId})");

                Console.WriteLine($"✅ Database initialized successfully. Max ID: {maxId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Database initialization failed: {ex.Message}");
            }
        }
    }
}
