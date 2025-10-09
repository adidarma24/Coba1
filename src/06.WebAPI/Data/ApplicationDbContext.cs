using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    /// <summary>
    /// Database context utama untuk aplikasi, bertanggung jawab untuk koneksi dan pemetaan entitas.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #region DbSets
        // =================================================================================
        // Mendefinisikan semua entitas yang akan dipetakan menjadi tabel di database.
        // =================================================================================
        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuCourse> MenuCourses { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<MenuCourse_Schedule> MenuCourse_Schedules { get; set; }
        #endregion

        #region Model Configuration
        // =================================================================================
        // Metode utama untuk mengonfigurasi model database.
        // =================================================================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Memanggil semua metode konfigurasi untuk setiap entitas
            ConfigureCategory(modelBuilder);
            ConfigureMenuCourse(modelBuilder);
            ConfigureSchedule(modelBuilder);
            ConfigureMenuCourseSchedule(modelBuilder);
            
            // Memanggil metode untuk mengisi data awal (seeding)
            SeedData(modelBuilder);
        }
        #endregion

        #region Entity Type Configurations
        // =================================================================================
        // Berisi semua konfigurasi detail untuk setiap entitas menggunakan Fluent API.
        // =================================================================================
        private void ConfigureCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
               entity.ToTable("Categories");
               entity.HasKey(e => e.Id);
               entity.Property(e => e.Name).IsRequired().HasMaxLength(100).HasComment("Nama kategori - harus unik");
               entity.Property(e => e.Image).HasMaxLength(500);
               entity.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");
               entity.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");
               entity.HasIndex(e => e.Name).IsUnique().HasDatabaseName("IX_Categories_Name");
            });
        }

        private void ConfigureMenuCourse(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuCourse>(entity =>
            {
                entity.ToTable("MenuCourses");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255).HasComment("Nama dari menu course");
                entity.Property(e => e.Description).HasMaxLength(1000).HasComment("Deskripsi detail dari menu course");
                entity.Property(e => e.Image).HasMaxLength(500).HasComment("URL path ke gambar menu course");
                entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)").HasComment("Harga menu course dalam format decimal");
                entity.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.CategoryId).IsRequired().HasComment("Foreign key ke tabel Categories");
                entity.HasIndex(e => e.Name).HasDatabaseName("IX_MenuCourses_Name");
                entity.HasIndex(e => e.CategoryId).HasDatabaseName("IX_MenuCourses_CategoryId");
                entity.HasIndex(e => e.Price).HasDatabaseName("IX_MenuCourses_Price");
                entity.HasOne(e => e.Category).WithMany(e => e.MenuCourses).HasForeignKey(e => e.CategoryId).OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_MenuCourses_Categories");
            });
        }

        private void ConfigureSchedule(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedules");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ScheduleDate).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");
            });
        }

        private void ConfigureMenuCourseSchedule(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuCourse_Schedule>(entity =>
            {
                entity.ToTable("MenuCourse_Schedules");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
                entity.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");
                entity.HasOne(e => e.MenuCourse).WithMany(mc => mc.MenuCourse_Schedules).HasForeignKey(e => e.MenuCourseId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Schedule).WithMany(s => s.MenuCourse_Schedules).HasForeignKey(e => e.ScheduleId).OnDelete(DeleteBehavior.Restrict);
            });
        }
        #endregion

        #region Database Seeding
        // =================================================================================
        // Menyediakan data awal untuk database saat migrasi pertama kali dijalankan.
        // =================================================================================
        private void SeedData(ModelBuilder modelBuilder)
        {
            var seedTime = new DateTime(2025, 10, 1, 0, 0, 0, DateTimeKind.Utc);
            
            // Data untuk Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Asian", CreatedAt = seedTime, UpdatedAt = seedTime, Image = null },
                new Category { Id = 2, Name = "Cold Drink", CreatedAt = seedTime, UpdatedAt = seedTime, Image = null },
                new Category { Id = 3, Name = "Cookies", CreatedAt = seedTime, UpdatedAt = seedTime, Image = null },
                new Category { Id = 4, Name = "Desert", CreatedAt = seedTime, UpdatedAt = seedTime, Image = null },
                new Category { Id = 5, Name = "Eastern", CreatedAt = seedTime, UpdatedAt = seedTime, Image = null },
                new Category { Id = 6, Name = "Hot Drink", CreatedAt = seedTime, UpdatedAt = seedTime, Image = null },
                new Category { Id = 7, Name = "Junkfood", CreatedAt = seedTime, UpdatedAt = seedTime, Image = null },
                new Category { Id = 8, Name = "Western", CreatedAt = seedTime, UpdatedAt = seedTime, Image = null }
            );

            // Data untuk MenuCourses
            modelBuilder.Entity<MenuCourse>().HasData(
                new MenuCourse { Id = 1, Name = "Nasi Goreng Spesial", Description = "Nasi goreng klasik dengan telur dan ayam.", Price = 25000m, CategoryId = 1, CreatedAt = seedTime, UpdatedAt = seedTime, Image = null },
                new MenuCourse { Id = 2, Name = "Es Teh Manis", Description = "Minuman teh dingin yang menyegarkan.", Price = 5000m, CategoryId = 2, CreatedAt = seedTime, UpdatedAt = seedTime, Image = null },
                new MenuCourse { Id = 3, Name = "Choco Chip Cookies", Description = "Kue kering dengan taburan choco chip.", Price = 15000m, CategoryId = 3, CreatedAt = seedTime, UpdatedAt = seedTime, Image = null },
                new MenuCourse { Id = 4, Name = "Pudding Coklat", Description = "Dessert puding rasa coklat.", Price = 12000m, CategoryId = 4, CreatedAt = seedTime, UpdatedAt = seedTime, Image = null },
                new MenuCourse { Id = 5, Name = "Kebab Turki", Description = "Daging panggang dengan sayuran dalam roti pita.", Price = 30000m, CategoryId = 5, CreatedAt = seedTime, UpdatedAt = seedTime, Image = null },
                new MenuCourse { Id = 6, Name = "Kopi Hitam", Description = "Kopi hitam panas tanpa gula.", Price = 8000m, CategoryId = 6, CreatedAt = seedTime, UpdatedAt = seedTime, Image = null },
                new MenuCourse { Id = 7, Name = "Kentang Goreng", Description = "Potongan kentang yang digoreng renyah.", Price = 18000m, CategoryId = 7, CreatedAt = seedTime, UpdatedAt = seedTime, Image = null },
                new MenuCourse { Id = 8, Name = "Spaghetti Bolognese", Description = "Pasta dengan saus daging tomat klasik.", Price = 45000m, CategoryId = 8, CreatedAt = seedTime, UpdatedAt = seedTime, Image = null }
            );

            // Data untuk Schedules
            modelBuilder.Entity<Schedule>().HasData(
                new Schedule { Id = 1, ScheduleDate = new DateTime(2025, 10, 20, 10, 0, 0, DateTimeKind.Utc), CreatedAt = seedTime, UpdatedAt = seedTime },
                new Schedule { Id = 2, ScheduleDate = new DateTime(2025, 10, 22, 14, 0, 0, DateTimeKind.Utc), CreatedAt = seedTime, UpdatedAt = seedTime },
                new Schedule { Id = 3, ScheduleDate = new DateTime(2025, 10, 25, 19, 0, 0, DateTimeKind.Utc), CreatedAt = seedTime, UpdatedAt = seedTime }
            );

            // Data untuk MenuCourse_Schedules
            modelBuilder.Entity<MenuCourse_Schedule>().HasData(
                new MenuCourse_Schedule { Id = 1, MenuCourseId = 1, ScheduleId = 1, AvailableSlot = 20, Status = "Active", CreatedAt = seedTime, UpdatedAt = seedTime },
                new MenuCourse_Schedule { Id = 2, MenuCourseId = 1, ScheduleId = 2, AvailableSlot = 20, Status = "Active", CreatedAt = seedTime, UpdatedAt = seedTime },
                new MenuCourse_Schedule { Id = 3, MenuCourseId = 2, ScheduleId = 3, AvailableSlot = 15, Status = "Active", CreatedAt = seedTime, UpdatedAt = seedTime }
            );
        }
        #endregion

        #region Automatic Timestamp Handling
        // =================================================================================
        // Otomatis memperbarui properti 'UpdatedAt' setiap kali entitas IAuditable disimpan.
        // =================================================================================
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditable && e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                ((IAuditable)entry.Entity).UpdatedAt = DateTime.UtcNow;
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
        #endregion
    }
}