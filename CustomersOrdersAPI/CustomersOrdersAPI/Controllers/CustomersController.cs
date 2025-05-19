using System.Text.Json;
using CustomersOrdersAPI.DTOs;
using CustomersOrdersAPI.Mappings;
using CustomersOrdersAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomersOrdersAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CustomersController> _logger;
    
    public CustomersController(IUnitOfWork unitOfWork, ILogger<CustomersController> logger)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCustomers(int page = 1, int pageSize = 100)
    {
        var customers = await _unitOfWork.CustomerRepository.GetAllAsync(page, pageSize);
        
        if (customers == null || !customers.Any())
        {
            return NotFound("No customers found.");
        }

        var customersDto = customers.ToDTOs();
        
        _logger.LogInformation(JsonSerializer.Serialize(new LogDTO<CustomerDTO>
        {
            Timestamp = DateTime.UtcNow,
            Level = "INFO",
            Message = "Customers retrieved successfully.",
            ExecutionTimeMs = 15,
            QuantityFound = customersDto.Count,
            UserId = "admin-api",
            Data = customersDto
        }));
        
        return Ok(customersDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(Guid id)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
        
        if (customer == null)
        {
            return NotFound($"Customer with ID {id} not found.");
        }

        var customerDto = customer.ToDTO();
        
        _logger.LogInformation(JsonSerializer.Serialize(new LogDTO<CustomerDTO>
        {
            Timestamp = DateTime.UtcNow,
            Level = "INFO",
            Message = $"Order '{id}' retrieved successfully.",
            ExecutionTimeMs = 15,
            QuantityFound = 1,
            UserId = "admin-api",
            Data = new List<CustomerDTO> { customerDto }
        }));
        
        return Ok(customerDto);
    }
}