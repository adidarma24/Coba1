using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

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

            // === RELASI MODEL ===

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.User)
                .WithMany(u => u.Invoices)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MyClass>()
                .HasOne(mc => mc.User)
                .WithMany(u => u.MyClasses)
                .HasForeignKey(mc => mc.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InvoiceMenuCourse>()
                .HasOne(imc => imc.Invoice)
                .WithMany(i => i.InvoiceMenuCourses)
                .HasForeignKey(imc => imc.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InvoiceMenuCourse>()
                .HasOne(imc => imc.MenuCourseSchedule)
                .WithMany(mcs => mcs.InvoiceMenuCourses)
                .HasForeignKey(imc => imc.MSId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MenuCourseSchedule>()
                .HasOne(mcs => mcs.Schedule)
                .WithMany(s => s.MenuCourseSchedules)
                .HasForeignKey(mcs => mcs.ScheduleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MenuCourseSchedule>()
                .HasOne(mcs => mcs.MenuCourse)
                .WithMany(mc => mc.MenuCourseSchedules)
                .HasForeignKey(mcs => mcs.MenuCourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MyClass>()
                .HasOne(mc => mc.MenuCourse)
                .WithMany(m => m.MyClasses)
                .HasForeignKey(mc => mc.MenuCourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MenuCourse>()
                .HasOne(m => m.Category)
                .WithMany(c => c.MenuCourses)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
