namespace MyApp.WebAPI.Models
{
    /// <summary>
    /// Interface untuk entitas yang memiliki timestamp pembuatan dan pembaruan.
    /// </summary>
    public interface IAuditable
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}