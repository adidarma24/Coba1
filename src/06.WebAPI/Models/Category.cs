namespace WebApplication1.Models
{
    /// <summary>
    /// Entity class untuk merepresentasikan Category dalam database
    /// </summary>
    public class Category : IAuditable // Direkomendasikan untuk implement IAuditable
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nama category - Required, unique, max 100 characters
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// URL path untuk gambar kategori (opsional)
        /// </summary>
        public string? Image { get; set; }

        // Properti Description dan IsActive DIHAPUS karena tidak ada di skema DB

        /// <summary>
        /// Timestamp kapan category ini dibuat
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Timestamp kapan category ini terakhir di-update
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Navigation property untuk relasi One-to-Many dengan MenuCourse
        /// </summary>
        public virtual ICollection<MenuCourse> MenuCourses { get; set; } = new List<MenuCourse>();
    }
}