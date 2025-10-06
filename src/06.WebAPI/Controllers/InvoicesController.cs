using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.DTOs;
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class InvoicesController : ControllerBase
  {
    private readonly AppDbContext _context;

    public InvoicesController(AppDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InvoiceDTO>>> GetAllInvoice()
    {
      var invoices = await _context.Invoices
                .Select(i => new InvoiceDTO
                {
                  InvoiceId = i.InvoiceId,
                  NoInvoice = i.NoInvoice,
                  Date = i.Date,
                  TotalCourse = i.TotalCourse,
                  TotalPrice = i.TotalPrice
                })
                .ToListAsync();

      return Ok(invoices);
    }

    [HttpGet("user/{id}")]
    public async Task<ActionResult<IEnumerable<InvoiceDTO>>> GetAllInvoiceUser(int id)
    {
      var invoices = await _context.Invoices
                .Where(i => i.UserId == id)
                .Select(i => new InvoiceDTO
                {
                  InvoiceId = i.InvoiceId,
                  NoInvoice = i.NoInvoice,
                  Date = i.Date,
                  TotalCourse = i.TotalCourse,
                  TotalPrice = i.TotalPrice
                })
                .ToListAsync();

      return Ok(invoices);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DetailInvoiceDTO>> GetInvoiceDetail(int id)
    {
      var invoice = await _context.Invoices
                .Where(i => i.InvoiceId == id)
                .Select(i => new DetailInvoiceDTO
                {
                  InvoiceId = i.InvoiceId,
                  NoInvoice = i.NoInvoice,
                  Date = i.Date,
                  TotalPrice = i.TotalPrice,
                  ListCourse = i.InvoiceMenuCourses
                        .Select(im => new CourseItemDTO
                        {
                          MenuCourseId = im.MenuCourseSchedule.MenuCourse.MenuCourseId,
                          Name = im.MenuCourseSchedule.MenuCourse.Name,
                          Category = im.MenuCourseSchedule.MenuCourse.Category.Name,
                          ScheduleDate = im.MenuCourseSchedule.Schedule.ScheduleDate,
                          Price = im.MenuCourseSchedule.MenuCourse.Price
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

      if (invoice == null)
        return NotFound();

      return Ok(invoice);
    }

    [HttpPost]
    public async Task<IActionResult> CreateInvoice(CreateInvoiceDTO createInvoiceDTO)
    {
      // check user not found
      var user = await _context.Users.FindAsync(createInvoiceDTO.UserId);
      if (user == null) return BadRequest("User not found.");

      // check not select any course
      if (createInvoiceDTO.MSId.Count == 0) return BadRequest("No course selected.");

      // check duplicate course
      var duplicateIds = createInvoiceDTO.MSId
                    .GroupBy(id => id)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();
      if (duplicateIds.Count != 0)
      {
        return BadRequest($"Duplicate course detected in request: {string.Join(", ", duplicateIds)}");
      }

      using var transaction = await _context.Database.BeginTransactionAsync();

      try
      {
        var schedules = await _context.MenuCourseSchedules
                    .Include(ms => ms.MenuCourse)
                    .Where(ms => createInvoiceDTO.MSId.Contains(ms.MSId))
                    .ToListAsync();

        // check if course not in database
        var missingIds = createInvoiceDTO.MSId.Except(schedules.Select(s => s.MSId)).ToList();
        if (missingIds.Count != 0)
        {
          var missingList = string.Join(", ", missingIds);
          return BadRequest($"Course not found for MSId: {missingList}");
        }

        // check if course already purchased
        var purchasedIds = await _context.MyClasses
            .Where(mc => mc.UserId == user.UserId && createInvoiceDTO.MSId.Contains(mc.MSId))
            .Select(mc => mc.MSId)
            .ToListAsync();
        if (purchasedIds.Count != 0)
        {
          var courseNames = schedules
              .Where(s => purchasedIds.Contains(s.MSId))
              .Select(s => s.MenuCourse.Name)
              .ToList();

          return BadRequest($"You have already purchased these course: {string.Join(", ", courseNames)}");
        }

        // check if course is full
        foreach (var schedule in schedules)
        {
          if (schedule.AvailableSlot <= 0)
            return BadRequest($"Schedule {schedule.MSId} ({schedule.MenuCourse.Name}) is full.");
        }

        // count total price and total course
        var totalPrice = schedules.Sum(s => s.MenuCourse.Price);
        var totalCourse = schedules.Count;

        // write invoice number
        var lastInvoice = await _context.Invoices
                    .OrderByDescending(i => i.InvoiceId)
                    .FirstOrDefaultAsync();
        int nextNumber = 1;
        if (lastInvoice != null && lastInvoice.NoInvoice.Length >= 8)
        {
          var numberPart = lastInvoice.NoInvoice.Substring(3);
          if (int.TryParse(numberPart, out int lastNumber))
            nextNumber = lastNumber + 1;
        }
        var invoiceNumber = $"SOU{nextNumber:D5}";

        var invoice = new Invoice
        {
          NoInvoice = invoiceNumber,
          Date = DateTime.UtcNow,
          TotalCourse = totalCourse,
          TotalPrice = totalPrice,
          UserId = user.UserId
        };

        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();

        foreach (var schedule in schedules)
        {
          schedule.AvailableSlot -= 1;

          if (schedule.AvailableSlot <= 0)
          {
            schedule.Status = MSStatus.Inactive;
          }

          var invoiceMenuCourse = new InvoiceMenuCourse
          {
            InvoiceId = invoice.InvoiceId,
            MSId = schedule.MSId
          };
          _context.InvoiceMenuCourses.Add(invoiceMenuCourse);

          var myClass = new MyClass
          {
            UserId = user.UserId,
            MSId = schedule.MSId
          };
          _context.MyClasses.Add(myClass);
        }

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return Ok(new
        {
          message = "Invoice created successfully",
          invoiceId = invoice.InvoiceId,
          invoiceNumber = invoice.NoInvoice,
          date = invoice.Date,
          totalCourse = invoice.TotalCourse,
          totalPrice = invoice.TotalPrice,
          userId = invoice.UserId
        });
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        return StatusCode(500, $"Error creating invoice: {ex.Message}");
      }
    }
  }
}