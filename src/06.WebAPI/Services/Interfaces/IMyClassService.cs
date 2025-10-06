using MyApp.WebAPI.DTOs;
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Services.Interfaces
{
  public interface IMyClassService
  {
    Task<PagedResult<MyClassDTO>> GetAllMyClassUserAsync(int userId, int pageNumber, int pageSize);
  }
}