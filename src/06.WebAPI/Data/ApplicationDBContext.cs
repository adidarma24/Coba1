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

    // USER - INVOICE (One to Many)
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.User)
                .WithMany(u => u.Invoices)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // USER - MYCLASS (One to Many)
            modelBuilder.Entity<MyClass>()
                .HasOne(mc => mc.User)
                .WithMany(u => u.MyClasses)
                .HasForeignKey(mc => mc.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // INVOICE - INVOICEMENUCOURSE (One to Many)
            modelBuilder.Entity<InvoiceMenuCourse>()
                .HasOne(imc => imc.Invoice)
                .WithMany(i => i.InvoiceMenuCourses)
                .HasForeignKey(imc => imc.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // INVOICEMENUCOURSE - MENUCOURSESCHEDULE (Many to One)
            modelBuilder.Entity<InvoiceMenuCourse>()
                .HasOne(imc => imc.MenuCourseSchedule)
                .WithMany(mcs => mcs.InvoiceMenuCourses)
                .HasForeignKey(imc => imc.MSId)
                .OnDelete(DeleteBehavior.Restrict);

            // MENUCOURSESCHEDULE - SCHEDULE (Many to One)
            modelBuilder.Entity<MenuCourseSchedule>()
                .HasOne(mcs => mcs.Schedule)
                .WithMany(s => s.MenuCourseSchedules)
                .HasForeignKey(mcs => mcs.ScheduleId)
                .OnDelete(DeleteBehavior.Restrict);

            // MENUCOURSE - MENUCOURSESCHEDULE (One to Many)
            modelBuilder.Entity<MenuCourseSchedule>()
                .HasOne(mcs => mcs.MenuCourse)
                .WithMany(mc => mc.MenuCourseSchedules)
                .HasForeignKey(mcs => mcs.MenuCourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // MENUCOURSE - MYCLASS (One to Many)
            modelBuilder.Entity<MyClass>()
                .HasOne(mc => mc.MenuCourse)
                .WithMany(m => m.MyClasses)
                .HasForeignKey(mc => mc.MenuCourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // MENUCOURSE - CATEGORY (Many to One)
            modelBuilder.Entity<MenuCourse>()
                .HasOne(m => m.Category)
                .WithMany(c => c.MenuCourses)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // --- USER ---
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Name = "Admin User",
                    Email = "admin@example.com",
                    Password = "admin123",
                    Row = "Admin",
                    Status = "Active",
                    CreatedAt = new DateTime(2025, 10, 6),
                    UpdatedAt = new DateTime(2025, 10, 6)
                },
                new User
                {
                    UserId = 2,
                    Name = "John Doe",
                    Email = "john@example.com",
                    Password = "password",
                    Row = "User",
                    Status = "Active",
                    CreatedAt = new DateTime(2025, 10, 6),
                    UpdatedAt = new DateTime(2025, 10, 6)
                }
            );

            // --- CATEGORY ---
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Asian", Image = "asian.svg", CreatedAt = new DateTime(2025, 10, 6), UpdatedAt = new DateTime(2025, 10, 6) },
                new Category { CategoryId = 2, Name = "Western", Image = "western.svg", CreatedAt = new DateTime(2025, 10, 6), UpdatedAt = new DateTime(2025, 10, 6) }
            );

            // --- MENU COURSE ---
            modelBuilder.Entity<MenuCourse>().HasData(
                new MenuCourse
                {
                    MenuCourseId = 1,
                    Name = "Tomyum",
                    Image = "tomyum.svg",
                    Price = 100000,
                    Description = "tomyum",
                    CategoryId = 1,
                    CreatedAt = new DateTime(2025, 10, 6),
                    UpdatedAt = new DateTime(2025, 10, 6)
                },
                new MenuCourse
                {
                    MenuCourseId = 2,
                    Name = "Pizza",
                    Image = "pizza.svg",
                    Price = 150000,
                    Description = "Learn design principles",
                    CategoryId = 2,
                    CreatedAt = new DateTime(2025, 10, 6),
                    UpdatedAt = new DateTime(2025, 10, 6)
                }
            );

            // --- SCHEDULE ---
            modelBuilder.Entity<Schedule>().HasData(
                new Schedule
                {
                    ScheduleId = 1,
                    ScheduleDate = new DateTime(2025, 10, 6).AddDays(3),
                    CreatedAt = new DateTime(2025, 10, 6),
                    UpdatedAt = new DateTime(2025, 10, 6)
                },
                new Schedule
                {
                    ScheduleId = 2,
                    ScheduleDate = new DateTime(2025, 10, 6).AddDays(7),
                    CreatedAt = new DateTime(2025, 10, 6),
                    UpdatedAt = new DateTime(2025, 10, 6)
                }
            );

            // --- MENUCOURSE_SCHEDULE ---
            modelBuilder.Entity<MenuCourseSchedule>().HasData(
                new MenuCourseSchedule
                {
                    MSId = 1,
                    MenuCourseId = 1,
                    ScheduleId = 1,
                    Available = 10,
                    Status = "Active",
                    CreatedAt = new DateTime(2025, 10, 6),
                    UpdatedAt = new DateTime(2025, 10, 6)
                },
                new MenuCourseSchedule
                {
                    MSId = 2,
                    MenuCourseId = 2,
                    ScheduleId = 2,
                    Available = 8,
                    Status = "Active",
                    CreatedAt = new DateTime(2025, 10, 6),
                    UpdatedAt = new DateTime(2025, 10, 6)
                }
            );

            // --- PAYMENT METHOD ---
            modelBuilder.Entity<PaymentMethod>().HasData(
                new PaymentMethod { PaymentMethodId = 1, Name = "BCA", Logo = "bca.png", Status = "Active", CreatedAt = new DateTime(2025, 10, 6), UpdatedAt = new DateTime(2025, 10, 6) },
                new PaymentMethod { PaymentMethodId = 2, Name = "GoPay", Logo = "gopay.png", Status = "Active", CreatedAt = new DateTime(2025, 10, 6), UpdatedAt = new DateTime(2025, 10, 6) }
            );

            // --- INVOICE ---
            modelBuilder.Entity<Invoice>().HasData(
                new Invoice
                {
                    InvoiceId = 1,
                    NoInvoice = "INV-001",
                    Date = new DateTime(2025, 10, 6),
                    TotalCourse = 1,
                    TotalPrice = 100000,
                    UserId = 2,
                    CreatedAt = new DateTime(2025, 10, 6),
                    UpdatedAt = new DateTime(2025, 10, 6)
                }
            );

            // --- INVOICE_MENUCOURSE ---
            modelBuilder.Entity<InvoiceMenuCourse>().HasData(
                new InvoiceMenuCourse
                {
                    IMId = 1,
                    InvoiceId = 1,
                    MSId = 1,
                    CreatedAt = new DateTime(2025, 10, 6),
                    UpdatedAt = new DateTime(2025, 10, 6)
                }
            );

            // --- MYCLASS ---
            modelBuilder.Entity<MyClass>().HasData(
                new MyClass
                {
                    MyClassId = 1,
                    UserId = 2,
                    MenuCourseId = 1,
                    CreatedAt = new DateTime(2025, 10, 6),
                    UpdatedAt = new DateTime(2025, 10, 6)
                }
            );
        }
    }
}
