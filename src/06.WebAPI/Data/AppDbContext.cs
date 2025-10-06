using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
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
          .Where(e => e.Entity is BaseModel && (
              e.State == EntityState.Added ||
              e.State == EntityState.Modified));

      foreach (var entry in entries)
      {
        ((BaseModel)entry.Entity).UpdatedAt = DateTime.UtcNow;

        if (entry.State == EntityState.Added)
          ((BaseModel)entry.Entity).CreatedAt = DateTime.UtcNow;
      }

      return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      var entries = ChangeTracker.Entries()
          .Where(e => e.Entity is BaseModel && (
              e.State == EntityState.Added ||
              e.State == EntityState.Modified));

      foreach (var entry in entries)
      {
        ((BaseModel)entry.Entity).UpdatedAt = DateTime.UtcNow;
        if (entry.State == EntityState.Added)
          ((BaseModel)entry.Entity).CreatedAt = DateTime.UtcNow;
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

      SeedData(modelBuilder);
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
                .HasForeignKey(i => i.UserId)
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
                .HasForeignKey(mc => mc.UserId)
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

    private static void SeedData(ModelBuilder modelBuilder)
    {
      var seedDate = new DateTime(2025, 10, 5);

      modelBuilder.Entity<Category>().HasData(
          new Category
          {
            CategoryId = 1,
            Name = "Asian",
            Image = "asian.svg",
            CreatedAt = seedDate,
            UpdatedAt = seedDate
          },
          new Category
          {
            CategoryId = 2,
            Name = "Cold Drink",
            Image = "coldDrink.svg",
            CreatedAt = seedDate,
            UpdatedAt = seedDate
          },
          new Category
          {
            CategoryId = 3,
            Name = "Cookies",
            Image = "cookies.svg",
            CreatedAt = seedDate,
            UpdatedAt = seedDate
          }
      );

      modelBuilder.Entity<PaymentMethod>().HasData(
          new PaymentMethod
          {
            PaymentMethodId = 1,
            Name = "Gopay",
            Logo = "gopay.svg",
            Status = PaymentStatus.Active,
            CreatedAt = seedDate,
            UpdatedAt = seedDate
          },
          new PaymentMethod
          {
            PaymentMethodId = 2,
            Name = "Dana",
            Logo = "dana.svg",
            Status = PaymentStatus.Inactive,
            CreatedAt = seedDate,
            UpdatedAt = seedDate
          }
      );

      modelBuilder.Entity<User>().HasData(
        new User
        {
          UserId = 1,
          Name = "Admin User",
          Email = "admin@example.com",
          Password = "hashed_password_1",
          Role = UserRole.Admin,
          Status = UserStatus.Active,
          CreatedAt = seedDate,
          UpdatedAt = seedDate
        },
        new User
        {
          UserId = 2,
          Name = "Alice",
          Email = "alice@example.com",
          Password = "hashed_password_2",
          Role = UserRole.User,
          Status = UserStatus.Active,
          CreatedAt = seedDate,
          UpdatedAt = seedDate
        },
        new User
        {
          UserId = 3,
          Name = "Bob",
          Email = "bob@example.com",
          Password = "hashed_password_3",
          Role = UserRole.User,
          Status = UserStatus.Inactive,
          CreatedAt = seedDate,
          UpdatedAt = seedDate
        }
      );

      modelBuilder.Entity<MenuCourse>().HasData(
        new MenuCourse
        {
          MenuCourseId = 1,
          Name = "Tom Yum Thailand",
          Image = "tomyum.jpg",
          Price = 450000m,
          Description = "Tom Yum dari Thailand",
          CategoryId = 1,
          CreatedAt = seedDate,
          UpdatedAt = seedDate
        },
        new MenuCourse
        {
          MenuCourseId = 2,
          Name = "Strawberry Float",
          Image = "strawberry_float.jpg",
          Price = 150000m,
          Description = "Minuman rasa strawberry",
          CategoryId = 2,
          CreatedAt = seedDate,
          UpdatedAt = seedDate
        },
        new MenuCourse
        {
          MenuCourseId = 3,
          Name = "Chocholate Cookies",
          Image = "cookies.jpg",
          Price = 200000m,
          Description = "Chocholate Cookies",
          CategoryId = 3,
          CreatedAt = seedDate,
          UpdatedAt = seedDate
        },
        new MenuCourse
        {
          MenuCourseId = 4,
          Name = "Soto Banjar Limau Kuit",
          Image = "greentea_cheesecake.jpg",
          Price = 150000m,
          Description = "Soto Banjar Limau Kuit",
          CategoryId = 1,
          CreatedAt = seedDate,
          UpdatedAt = seedDate
        },
        new MenuCourse
        {
          MenuCourseId = 5,
          Name = "Green Tea Cheesecake",
          Image = "cheesecake.jpg",
          Price = 400000m,
          Description = "Green Tea Cheesecake",
          CategoryId = 2,
          CreatedAt = seedDate,
          UpdatedAt = seedDate
        },
        new MenuCourse
        {
          MenuCourseId = 6,
          Name = "Italian Spagetti Bolognese",
          Image = "spagetti.jpg",
          Price = 450000m,
          Description = "Spagetti",
          CategoryId = 3,
          CreatedAt = seedDate,
          UpdatedAt = seedDate
        }
      );

      modelBuilder.Entity<Schedule>().HasData(
        new Schedule
        {
          ScheduleId = 1,
          ScheduleDate = new DateTime(2025, 10, 6, 9, 0, 0),
          CreatedAt = seedDate,
          UpdatedAt = seedDate
        },
        new Schedule
        {
          ScheduleId = 2,
          ScheduleDate = new DateTime(2025, 10, 7, 13, 0, 0),
          CreatedAt = seedDate,
          UpdatedAt = seedDate
        },
        new Schedule
        {
          ScheduleId = 3,
          ScheduleDate = new DateTime(2025, 10, 8, 10, 0, 0),
          CreatedAt = seedDate,
          UpdatedAt = seedDate
        },
        new Schedule
        {
          ScheduleId = 4,
          ScheduleDate = new DateTime(2025, 10, 9, 14, 0, 0),
          CreatedAt = seedDate,
          UpdatedAt = seedDate
        }
      );

      modelBuilder.Entity<MenuCourseSchedule>().HasData(
        new MenuCourseSchedule { MSId = 1, MenuCourseId = 1, ScheduleId = 1, AvailableSlot = 10, Status = MSStatus.Active, CreatedAt = seedDate, UpdatedAt = seedDate },
        new MenuCourseSchedule { MSId = 2, MenuCourseId = 1, ScheduleId = 2, AvailableSlot = 12, Status = MSStatus.Active, CreatedAt = seedDate, UpdatedAt = seedDate },
        new MenuCourseSchedule { MSId = 3, MenuCourseId = 1, ScheduleId = 3, AvailableSlot = 0, Status = MSStatus.Inactive, CreatedAt = seedDate, UpdatedAt = seedDate },

        new MenuCourseSchedule { MSId = 4, MenuCourseId = 2, ScheduleId = 2, AvailableSlot = 10, Status = MSStatus.Active, CreatedAt = seedDate, UpdatedAt = seedDate },
        new MenuCourseSchedule { MSId = 5, MenuCourseId = 2, ScheduleId = 3, AvailableSlot = 0, Status = MSStatus.Inactive, CreatedAt = seedDate, UpdatedAt = seedDate },
        new MenuCourseSchedule { MSId = 6, MenuCourseId = 2, ScheduleId = 4, AvailableSlot = 9, Status = MSStatus.Active, CreatedAt = seedDate, UpdatedAt = seedDate },

        new MenuCourseSchedule { MSId = 7, MenuCourseId = 3, ScheduleId = 1, AvailableSlot = 1, Status = MSStatus.Active, CreatedAt = seedDate, UpdatedAt = seedDate },
        new MenuCourseSchedule { MSId = 8, MenuCourseId = 3, ScheduleId = 2, AvailableSlot = 0, Status = MSStatus.Inactive, CreatedAt = seedDate, UpdatedAt = seedDate },

        new MenuCourseSchedule { MSId = 9, MenuCourseId = 4, ScheduleId = 3, AvailableSlot = 10, Status = MSStatus.Active, CreatedAt = seedDate, UpdatedAt = seedDate },
        new MenuCourseSchedule { MSId = 10, MenuCourseId = 4, ScheduleId = 4, AvailableSlot = 5, Status = MSStatus.Active, CreatedAt = seedDate, UpdatedAt = seedDate },

        new MenuCourseSchedule { MSId = 11, MenuCourseId = 5, ScheduleId = 1, AvailableSlot = 1, Status = MSStatus.Active, CreatedAt = seedDate, UpdatedAt = seedDate },
        new MenuCourseSchedule { MSId = 12, MenuCourseId = 5, ScheduleId = 2, AvailableSlot = 2, Status = MSStatus.Active, CreatedAt = seedDate, UpdatedAt = seedDate },

        new MenuCourseSchedule { MSId = 13, MenuCourseId = 6, ScheduleId = 4, AvailableSlot = 10, Status = MSStatus.Active, CreatedAt = seedDate, UpdatedAt = seedDate }
    );

    }
  }
}
