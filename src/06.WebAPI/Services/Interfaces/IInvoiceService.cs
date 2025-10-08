using MyApp.WebAPI.DTOs;
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Services.Interfaces
{
  public interface IInvoiceService
  {
    Task<PagedResult<InvoiceDTO>> GetAllInvoicesAsync(int pageNumber, int pageSize);
    Task<IEnumerable<InvoiceDTO>> GetUserInvoicesAsync(int userId);
    Task<DetailInvoiceDTO?> GetInvoiceDetailAsync(int id);
    Task<object> CreateInvoiceAsync(CreateInvoiceDTO createInvoiceDTO);
  }
}