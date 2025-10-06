using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<InvoiceMenuCourse> InvoiceMenuCourses => Set<InvoiceMenuCourse>();
        public DbSet<MenuCourseSchedule> MenuCourseSchedules => Set<MenuCourseSchedule>();
        public DbSet<MenuCourse> MenuCourses => Set<MenuCourse>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<MyClass> MyClasses => Set<MyClass>();
        public DbSet<Schedule> Schedules => Set<Schedule>();
        public DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // // Optional: konfigurasi tambahan (misalnya delete behavior)
            // modelBuilder.Entity<User>()
            //     .HasMany(u => u.Invoices)
            //     .WithOne(i => i.User)
            //     .HasForeignKey(i => i.UserId)
            //     .OnDelete(DeleteBehavior.Restrict);

            // modelBuilder.Entity<User>()
            //     .HasMany(u => u.MyClasses)
            //     .WithOne(c => c.User)
            //     .HasForeignKey(c => c.UserId)
            //     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
