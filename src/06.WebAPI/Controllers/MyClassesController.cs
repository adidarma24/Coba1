using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.DTOs;
using MyApp.WebAPI.Models;
using MyApp.WebAPI.Services.Interfaces;

namespace MyApp.WebAPI.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class MyClassesController : ControllerBase
  {
    private readonly IMyClassService _myClassService;

    public MyClassesController(IMyClassService myClassService)
    {
      _myClassService = myClassService;
    }

    [HttpGet("user/{id}")]
    public async Task<ActionResult<ApiResponse<PagedResult<MyClassDTO>>>> GetAllMyClassUser(
      int id,
      [FromQuery] int pageNumber = 1,
      [FromQuery] int pageSize = 10
    )
    {
      var result = await _myClassService.GetAllMyClassUserAsync(id, pageNumber, pageSize);

      return Ok(new ApiResponse<PagedResult<MyClassDTO>>
      {
        Success = true,
        Data = result,
        Message = "My Classes retrieved successfully."
      });
    }
  }
}