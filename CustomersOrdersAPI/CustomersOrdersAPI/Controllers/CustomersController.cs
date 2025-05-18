using CustomersOrdersAPI.Mappings;
using CustomersOrdersAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomersOrdersAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CustomersController(IUnitOfWork unitOfWork)
    {
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
        
        return Ok(customerDto);
    }
}