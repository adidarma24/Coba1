using System.ComponentModel.DataAnnotations;

namespace MyApp.WebAPI.DTO.Users
{
    public class CreateUserRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = "User123!"; // default optional

        public string Role { get; set; } = "User";

        public bool IsActive { get; set; } = true;
    }
}
