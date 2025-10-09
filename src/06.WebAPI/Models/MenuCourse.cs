namespace WebApplication1.Models
{
    public class MenuCourse : IAuditable // Direkomendasikan untuk implement IAuditable
    {
        // DIPERBAIKI: Nama PK disamakan menjadi 'Id'
        public int Id { get; set; }

        // Atribut [Required] dll. dihapus, diatur di DbContext
        public string Name { get; set; }

        public string? Image { get; set; }

        // DIPERBAIKI: Tipe data diubah ke decimal
        public decimal Price { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int CategoryId { get; set; }

        // Atribut [ForeignKey] dihapus, diatur di DbContext
        public virtual Category Category { get; set; } = null!;

        // TAMBAHKAN INI: Satu MenuCourse bisa memiliki banyak jadwal
        public virtual ICollection<MenuCourse_Schedule> MenuCourse_Schedules { get; set; } = new List<MenuCourse_Schedule>();
    }
}