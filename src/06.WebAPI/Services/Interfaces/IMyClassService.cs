using MyApp.WebAPI.DTOs;
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Services.Interfaces
{
  public interface IMyClassService
  {
    Task<IEnumerable<MyClassDTO>> GetAllMyClassUserAsync(int userId);
  }
}