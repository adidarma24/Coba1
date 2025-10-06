using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Extensions
{
  public static class IQueryableExtensions
  {
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize)
    {
      var totalRecords = await query.CountAsync();
      var items = await query
          .Skip((pageNumber - 1) * pageSize)
          .Take(pageSize)
          .ToListAsync();

      return new PagedResult<T>
      {
        PageNumber = pageNumber,
        PageSize = pageSize,
        TotalRecords = totalRecords,
        Items = items
      };
    }
  }
}
