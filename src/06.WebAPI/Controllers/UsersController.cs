using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.DTO.Users;
using MyApp.WebAPI.Services.Interfaces;

namespace MyApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(int page = 1, int pageSize = 10)
            => Ok(await _userService.GetAllUsersAsync(page, pageSize));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
            => Ok(await _userService.GetUserByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
            => Ok(await _userService.CreateUserAsync(request));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest request)
            => Ok(await _userService.UpdateUserAsync(id, request));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
            => Ok(await _userService.DeleteUserAsync(id));
    }
}
