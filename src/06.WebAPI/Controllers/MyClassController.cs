using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.DTOs;
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class MyClassController : ControllerBase
  {
    private readonly AppDbContext _context;

    public MyClassController(AppDbContext context)
    {
      _context = context;
    }

    [HttpGet("user/{id}")]
    public async Task<ActionResult<IEnumerable<MyClassDTO>>> GetAllMyClassUser(int id)
    {
      var myClasses = await _context.MyClasses
                .Where(i => i.UserId == id)
                .Select(i => new MyClassDTO
                {
                  MenuCourseId = i.MenuCourseSchedule.MenuCourse.MenuCourseId,
                  Name = i.MenuCourseSchedule.MenuCourse.Name,
                  Image = i.MenuCourseSchedule.MenuCourse.Image,
                  Category = i.MenuCourseSchedule.MenuCourse.Category.Name,
                  Schedule = i.MenuCourseSchedule.Schedule.ScheduleDate
                })
                .ToListAsync();

      return Ok(myClasses);
    }
  }
}