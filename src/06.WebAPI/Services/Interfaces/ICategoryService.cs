
usingMyApp.WebAPI.DTOs;

namespaceMyApp.WebAPI.Services
{
    /// <summary>
    /// Interface (kontrak) untuk Category Service
    /// Mendefinisikan semua operasi yang bisa dilakukan terkait data Category
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Mengambil semua kategori yang ada
        /// </summary>
        /// <returns>Sebuah koleksi (IEnumerable) dari CategoryDto</returns>
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        
        /// <summary>
        /// Mencari dan mengambil satu kategori berdasarkan ID-nya
        /// </summary>
        /// <param name="id">ID dari kategori yang dicari</param>
        /// <returns>Sebuah CategoryDto jika ditemukan, atau null jika tidak</returns>
        Task<CategoryDto?> GetCategoryByIdAsync(int id);
        
        /// <summary>
        /// Membuat sebuah kategori baru berdasarkan data dari DTO
        /// </summary>
        /// <param name="createCategoryDto">Objek yang berisi data untuk kategori baru</param>
        /// <returns>Objek CategoryDto dari data yang baru saja dibuat</returns>
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        
        /// <summary>
        /// Memperbarui data sebuah kategori yang sudah ada
        /// </summary>
        /// <param name="id">ID dari kategori yang akan diupdate</param>
        /// <param name="updateCategoryDto">Objek yang berisi data pembaruan</param>
        /// <returns>Objek CategoryDto dari data yang sudah diupdate, atau null jika kategori tidak ditemukan</returns>
        Task<CategoryDto?> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto);
        
        /// <summary>
        /// Menghapus sebuah kategori berdasarkan ID-nya
        /// </summary>
        /// <param name="id">ID dari kategori yang akan dihapus</param>
        /// <returns>Boolean `true` jika berhasil dihapus, dan `false` jika kategori tidak ditemukan</returns>
        Task<bool> DeleteCategoryAsync(int id);
        
        /// <summary>
        /// Memeriksa apakah sebuah kategori dengan ID tertentu sudah ada
        /// </summary>
        /// <param name="id">ID dari kategori yang akan diperiksa</param>
        /// <returns>Boolean `true` jika kategori ada, dan `false` jika tidak</returns>
        Task<bool> CategoryExistsAsync(int id);
    }
}