// Import Entity Framework Core untuk database operations
using Microsoft.EntityFrameworkCore;
// Import ApplicationDbContext dari folder Data
using WebApplication1.Data;
// Import extension methods dari folder Extensions
using WebApplication1.Extensions;
// Import custom middleware dari folder Middleware
using WebApplication1.Middleware;
// Import FluentValidation core library
using FluentValidation;
// Import FluentValidation ASP.NET Core integration
using FluentValidation.AspNetCore;
// Import System.Reflection untuk assembly operations
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// ========== KONFIGURASI SERVICES (DEPENDENCY INJECTION) ==========

builder.Services.AddControllers();

// Daftarkan Database Context menggunakan extension method
builder.Services.AddDatabaseContext(builder.Configuration);

// Daftarkan AutoMapper untuk object-to-object mapping
builder.Services.AddAutoMapperProfiles();

// Konfigurasi FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidators();

// Daftarkan semua business services (CategoryService, MenuCourseService, dll.)
builder.Services.AddApplicationServices();

// Konfigurasi CORS untuk mengizinkan cross-origin requests
builder.Services.AddCorsPolicy();

// ========== KONFIGURASI SWAGGER/OPENAPI ==========

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // DIPERBAIKI: Judul dan deskripsi disesuaikan dengan konteks aplikasi saat ini
    c.SwaggerDoc("v1", new() { 
        Title = "Course API", // Nama API
        Version = "v1", // Versi API
        Description = "A simple API for managing courses and categories", // Deskripsi API
        Contact = new() { // Info kontak developer
            Name = "Developer Team", 
            Email = "dev@company.com" 
        }
    });
    
    // Include XML comments untuk dokumentasi yang lebih detail
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// ========== BUILD APLIKASI ==========

var app = builder.Build();

// ========== SEED DATABASE ==========

await SeedDatabase(app);

// ========== KONFIGURASI HTTP REQUEST PIPELINE ==========

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // DIPERBAIKI: Nama endpoint disesuaikan
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Course API V1");
        c.RoutePrefix = string.Empty;
    });
}

// ========== MIDDLEWARE PIPELINE (URUTAN PENTING!) ==========

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// ========== START APLIKASI ==========

app.Run();

// ========== HELPER METHODS ==========

static async Task SeedDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    
    // DIPERBAIKI: Komentar disesuaikan dengan ApplicationDbContext
    // Ambil instance ApplicationDbContext dari DI container
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    await context.Database.EnsureCreatedAsync();
    
    // DIPERBAIKI: Komentar disesuaikan dengan ApplicationDbContext
    // Note: Data seed sudah dikonfigurasi di ApplicationDbContext.OnModelCreating
}