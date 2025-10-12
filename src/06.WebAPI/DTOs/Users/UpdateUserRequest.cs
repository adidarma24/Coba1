using System.ComponentModel.DataAnnotations;

namespace MyApp.WebAPI.DTO.Users
{
    public class UpdateUserRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
