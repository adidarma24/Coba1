using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.DTOs;
using MyApp.WebAPI.Models;
using MyApp.WebAPI.Exceptions;
using MyApp.WebAPI.Extensions;
using MyApp.WebAPI.Services.Interfaces;
using System.Security.Claims;

namespace MyApp.WebAPI.Services
{
  public class MyClassService : IMyClassService
  {
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MyClassService(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
      _context = context;
      _mapper = mapper;
      _httpContextAccessor = httpContextAccessor;
    }

    private int GetCurrentUserId()
    {
      var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      if (userIdClaim == null) throw new UnauthorizedAccessException("User not authenticated.");
      return int.Parse(userIdClaim);
    }

    private bool IsAdmin()
    {
      return _httpContextAccessor.HttpContext?.User?.IsInRole("Admin") ?? false;
    }

    public async Task<IEnumerable<MyClassDTO>> GetAllMyClassUserAsync(int userId)
    {
      var currentUserId = GetCurrentUserId();
      var isAdmin = IsAdmin();

      if (!isAdmin && currentUserId != userId)
        throw new ForbiddenException("You can only access your own class.");


      var user = await _context.Users.AnyAsync(u => u.Id == userId);
      if (!user)
        throw new NotFoundException($"User with ID {userId} not found");

      var myClasses = await _context.MyClasses
                .AsNoTracking()
                .Where(i => i.UserIdRef == userId)
                .ProjectTo<MyClassDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

      return myClasses;
    }
  }
}