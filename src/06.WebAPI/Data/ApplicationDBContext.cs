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

    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceMenuCourse> InvoiceMenuCourses { get; set; }
    public DbSet<MenuCourse> MenuCourses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<MenuCourseSchedule> MenuCourseSchedules { get; set; }
    public DbSet<MyClass> MyClasses { get; set; }



    public override int SaveChanges()
    {
      var entries = ChangeTracker.Entries()
        .Where(e => e.Entity is BaseModel || e.Entity is User)
        .ToList();

      foreach (var entry in entries)
      {
        if (entry.State == EntityState.Added)
        {
          if (entry.Property("CreatedAt") != null)
            entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
        }

        if (entry.State == EntityState.Modified)
        {
          if (entry.Property("UpdatedAt") != null)
            entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
        }
      }

      return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      var entries = ChangeTracker.Entries()
        .Where(e => e.Entity is BaseModel || e.Entity is User)
        .ToList();

      foreach (var entry in entries)
      {
        if (entry.State == EntityState.Added)
        {
          if (entry.Property("CreatedAt") != null)
            entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
        }

        if (entry.State == EntityState.Modified)
        {
          if (entry.Property("UpdatedAt") != null)
            entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
        }
      }

      return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      ConfigurePrimaryKey(modelBuilder);

      ConfigureRelationships(modelBuilder);

      ConfigureDecimalPrecision(modelBuilder);

      ConfigureIndexes(modelBuilder);
    }

    private void ConfigurePrimaryKey(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<MenuCourseSchedule>()
          .HasKey(ms => ms.MSId);

      modelBuilder.Entity<InvoiceMenuCourse>()
          .HasKey(im => im.IMId);
    }

    private void ConfigureRelationships(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Invoice>()
                .HasOne(i => i.User)
                .WithMany(u => u.Invoices)
                .HasForeignKey(i => i.UserIdRef)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<MenuCourse>()
                .HasOne(mc => mc.Category)
                .WithMany(c => c.MenuCourses)
                .HasForeignKey(mc => mc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<MenuCourseSchedule>()
                .HasOne(ms => ms.MenuCourse)
                .WithMany(mc => mc.MenuCourseSchedules)
                .HasForeignKey(ms => ms.MenuCourseId)
                .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<MenuCourseSchedule>()
                .HasOne(ms => ms.Schedule)
                .WithMany(s => s.MenuCourseSchedules)
                .HasForeignKey(ms => ms.ScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<InvoiceMenuCourse>()
                .HasOne(im => im.Invoice)
                .WithMany(i => i.InvoiceMenuCourses)
                .HasForeignKey(im => im.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<InvoiceMenuCourse>()
                .HasOne(im => im.MenuCourseSchedule)
                .WithMany(ms => ms.InvoiceMenuCourses)
                .HasForeignKey(im => im.MSId)
                .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<MyClass>()
                .HasOne(mc => mc.User)
                .WithMany(u => u.MyClasses)
                .HasForeignKey(mc => mc.UserIdRef)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<MyClass>()
                .HasOne(mc => mc.MenuCourseSchedule)
                .WithMany(ms => ms.MyClasses)
                .HasForeignKey(mc => mc.MSId)
                .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureDecimalPrecision(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<MenuCourse>()
          .Property(p => p.Price)
          .HasPrecision(18, 2);

      modelBuilder.Entity<Invoice>()
          .Property(p => p.TotalPrice)
          .HasPrecision(18, 2);
    }

    private static void ConfigureIndexes(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>()
          .HasIndex(u => u.Email)
          .IsUnique();

      modelBuilder.Entity<Invoice>()
          .HasIndex(i => i.NoInvoice)
          .IsUnique();

      modelBuilder.Entity<Category>()
          .HasIndex(c => c.Name)
          .IsUnique();

      modelBuilder.Entity<MenuCourse>()
          .HasIndex(mc => new { mc.Name, mc.CategoryId })
          .IsUnique();
    }
  }
}
