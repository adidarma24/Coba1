using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Services;
using FluentValidation;
using System.Reflection;
using WebApplication1.Middleware; // Namespace untuk middleware jika Anda memindahkannya nanti

namespace WebApplication1.Extensions
{
    public static class ServiceCollectionExtensions
    {
        // ... (Metode AddApplicationServices, AddValidators, dll. tidak diubah) ...

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IMenuCourseService, MenuCourseService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<IMenuCourseScheduleService, MenuCourseScheduleService>();
            return services;
        }

        /// <summary>
        /// Method untuk konfigurasi database context dan koneksinya secara dinamis.
        /// </summary>
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            // Baca pengaturan 'UseInMemoryDatabase' dari appsettings.json
            var useInMemoryDatabase = configuration.GetValue<bool>("UseInMemoryDatabase");

            if (useInMemoryDatabase)
            {
                // Jika true, gunakan In-Memory Database.
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("CourseApiDb"));
                
                Console.WriteLine("Using In-Memory Database.");
            }
            else
            {
                // Jika false, gunakan SQL Server.
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));

                Console.WriteLine("Using SQL Server Database.");
            }
            
            return services;
        }
        
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            return services;
        }

        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}