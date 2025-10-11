using MyApp.WebAPI.DTO.Users;

namespace MyApp.WebAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<object> GetAllUsersAsync(int page, int pageSize);
        Task<object> GetUserByIdAsync(int id);
        Task<object> CreateUserAsync(CreateUserRequest request);
        Task<object> UpdateUserAsync(int id, UpdateUserRequest request);
        Task<object> DeleteUserAsync(int id);
    }
}
