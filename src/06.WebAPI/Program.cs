using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.Models;
using MyApp.WebAPI.Services.Interfaces;
using MyApp.WebAPI.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// ===============================================
// 1️⃣ Connection String & Database Configuration
// ===============================================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// ===============================================
// 2️⃣ Identity Configuration
// ===============================================
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    // ✅ Password rules
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;

    // ✅ User rules
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// ===============================================
// 3️⃣ Register Services (Dependency Injection)
// ===============================================
// Di sini kita daftarkan semua service yang kita pisahkan ke folder Services/
builder.Services.AddScoped<IUserService, UserService>();

// ===============================================
// 4️⃣ Tambahkan Controller, Swagger, dan Authorization
// ===============================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddOpenApi();

// ===============================================
// 5️⃣ Build App
// ===============================================
var app = builder.Build();

// ===============================================
// 6️⃣ Automatic Migration & Seeding
// ===============================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();

    await SeedData.InitializeAsync(services);
}

// ===============================================
// 7️⃣ Middleware Pipeline
// ===============================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
