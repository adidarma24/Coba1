using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.DTOs;
using MyApp.WebAPI.Models;
using MyApp.WebAPI.Services.Interfaces;
using MyApp.WebAPI.Configuration;

namespace MyApp.WebAPI.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class InvoicesController : ControllerBase
  {
    private readonly IInvoiceService _invoiceService;

    public InvoicesController(IInvoiceService invoiceService)
    {
      _invoiceService = invoiceService;
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]
    [ProducesResponseType(typeof(ApiResponse<InvoiceDTO>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 404)]
    public async Task<ActionResult<ApiResponse<PagedResult<InvoiceDTO>>>> GetAllInvoice(
      [FromQuery] int pageNumber = 1,
      [FromQuery] int pageSize = 10
    )
    {
      var result = await _invoiceService.GetAllInvoicesAsync(pageNumber, pageSize);
      return Ok(new ApiResponse<PagedResult<InvoiceDTO>>
      {
        Success = true,
        Data = result,
        Message = "Invoices retrieved successfully."
      });
    }

    [HttpGet("user/{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<InvoiceDTO>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 404)]
    public async Task<ActionResult<ApiResponse<IEnumerable<InvoiceDTO>>>> GetUserInvoice(int id)
    {
      var result = await _invoiceService.GetUserInvoicesAsync(id);
      return Ok(new ApiResponse<IEnumerable<InvoiceDTO>>
      {
        Success = true,
        Data = result,
        Message = "Invoices retrieved successfully."
      });
    }

    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<DetailInvoiceDTO>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 404)]
    public async Task<ActionResult<ApiResponse<DetailInvoiceDTO>>> GetInvoiceDetail(int id)
    {
      var result = await _invoiceService.GetInvoiceDetailAsync(id);
      return Ok(new ApiResponse<DetailInvoiceDTO>
      {
        Success = true,
        Data = result,
        Message = "Detail invoice retrieved successfully."
      });
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<object>), 201)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<ActionResult<ApiResponse<object>>> CreateInvoice(CreateInvoiceDTO createInvoiceDTO)
    {
      var response = await _invoiceService.CreateInvoiceAsync(createInvoiceDTO);
      return StatusCode(201, new ApiResponse<object>
      {
        Success = true,
        Data = response,
        Message = "Invoice created successfully."
      });
    }
  }
}