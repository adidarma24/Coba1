namespace WebApplication1.DTOs
{
    /// <summary>
    /// Class untuk menampung parameter query untuk endpoint pencarian Kategori.
    /// </summary>
    public class CategoryQueryParameters
    {
        /// <summary>
        /// Kata kunci untuk mencari nama kategori.
        /// </summary>
        public string? Name { get; set; }
    }
}