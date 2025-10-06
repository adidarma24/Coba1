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
  public class MyClassService : IMyClassService
  {
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public MyClassService(AppDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<PagedResult<MyClassDTO>> GetAllMyClassUserAsync(int userId, int pageNumber = 1, int pageSize = 10)
    {
      var myClasses = _context.MyClasses
                .AsNoTracking()
                .Where(i => i.UserId == userId)
                .ProjectTo<MyClassDTO>(_mapper.ConfigurationProvider);

      var pagedResult = await myClasses.ToPagedResultAsync(pageNumber, pageSize);

      if (!pagedResult.Items.Any())
        throw new NotFoundException($"User with with ID {userId} not found");

      return pagedResult;
    }
  }
}