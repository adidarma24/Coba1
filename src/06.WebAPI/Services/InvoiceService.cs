using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.DTOs;
using MyApp.WebAPI.Models;
using MyApp.WebAPI.Exceptions;
using MyApp.WebAPI.Extensions;
using MyApp.WebAPI.Services.Interfaces;

namespace MyApp.WebAPI.Services
{
  public class InvoiceService : IInvoiceService
  {
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public InvoiceService(AppDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<PagedResult<InvoiceDTO>> GetAllInvoicesAsync(int pageNumber = 1, int pageSize = 10)
    {
      var invoices = _context.Invoices
                .AsNoTracking()
                .ProjectTo<InvoiceDTO>(_mapper.ConfigurationProvider);

      return await invoices.ToPagedResultAsync(pageNumber, pageSize);
    }

    public async Task<PagedResult<InvoiceDTO>> GetUserInvoicesAsync(int userId, int pageNumber = 1, int pageSize = 10)
    {
      var invoices = _context.Invoices
                .AsNoTracking()
                .Where(i => i.UserId == userId)
                .ProjectTo<InvoiceDTO>(_mapper.ConfigurationProvider);

      var pagedResult = await invoices.ToPagedResultAsync(pageNumber, pageSize);

      if (!pagedResult.Items.Any())
        throw new NotFoundException($"User with with ID {userId} not found");

      return pagedResult;
    }

    public async Task<DetailInvoiceDTO?> GetInvoiceDetailAsync(int id)
    {
      var invoice = await _context.Invoices
              .Where(i => i.InvoiceId == id)
              .ProjectTo<DetailInvoiceDTO>(_mapper.ConfigurationProvider)
              .FirstOrDefaultAsync();

      if (invoice == null)
        throw new NotFoundException($"Invoice with ID {id} not found");

      return invoice;
    }

    public async Task<object> CreateInvoiceAsync(CreateInvoiceDTO createInvoiceDTO)
    {
      // check user not found
      var user = await _context.Users.FindAsync(createInvoiceDTO.UserId);
      if (user == null) throw new NotFoundException("User not found.", new { createInvoiceDTO.UserId });

      // check not select any course
      if (createInvoiceDTO.MSId == null || createInvoiceDTO.MSId.Count == 0)
        throw new ValidationException("No course selected.");

      // check duplicate course
      var duplicateIds = createInvoiceDTO.MSId
                    .GroupBy(id => id)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();
      if (duplicateIds.Count != 0)
        throw new ValidationException("Duplicate courses detected.", new { duplicateIds });

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
          throw new NotFoundException("Some courses were not found.", new { missingIds });

        // check if course already purchased
        var purchasedIds = await _context.MyClasses
            .Where(mc => mc.UserId == user.UserId && createInvoiceDTO.MSId.Contains(mc.MSId))
            .Select(mc => mc.MSId)
            .ToListAsync();
        if (purchasedIds.Count != 0)
        {
          var courses = schedules
              .Where(s => purchasedIds.Contains(s.MSId))
              .Select(s => new
              {
                MSId = s.MSId,
                CourseName = s.MenuCourse.Name
              })
              .ToList();
          throw new ValidationException("You already purchased these courses.", new { courses });
        }

        // check if course is full
        foreach (var schedule in schedules)
        {
          if (schedule.AvailableSlot <= 0 || schedule.Status == MSStatus.Inactive)
            throw new ValidationException($"Schedule {schedule.MSId} ({schedule.MenuCourse.Name}) is full.");
        }

        var totalPrice = schedules.Sum(s => s.MenuCourse.Price);
        var totalCourse = schedules.Count;

        // invoice number
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
          if (schedule.AvailableSlot <= 0) schedule.Status = MSStatus.Inactive;

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

        return new
        {
          invoiceId = invoice.InvoiceId,
          invoiceNumber = invoice.NoInvoice,
          date = invoice.Date,
          totalCourse = invoice.TotalCourse,
          totalPrice = invoice.TotalPrice,
          userId = invoice.UserId
        };
      }
      catch
      {
        await transaction.RollbackAsync();
        throw;
      }
    }
  }
}