using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<DetailInvoiceDTO>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 404)]
    public async Task<ActionResult<ApiResponse<IEnumerable<MyClassDTO>>>> GetAllMyClassUser(int id)
    {
      var result = await _myClassService.GetAllMyClassUserAsync(id);

      return Ok(new ApiResponse<IEnumerable<MyClassDTO>>
      {
        Success = true,
        Data = result,
        Message = "My Classes retrieved successfully."
      });
    }
  }
}