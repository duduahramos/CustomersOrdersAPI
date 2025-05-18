using CustomersOrdersAPI.Mappings;
using CustomersOrdersAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomersOrdersAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public OrdersController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetOrders(int page, int pageSize)
    {
        var orders = await _unitOfWork.OrderRepository.GetAllAsync(page, pageSize);
        
        if (orders == null || !orders.Any())
        {
            return NotFound("No orders found.");
        }

        var ordersDto = orders.ToDTOs();
        
        return Ok(ordersDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
        
        if (order == null)
        {
            return NotFound($"Order with ID {id} not found.");
        }

        var orderDto = order.ToDTO();
        
        return Ok(orderDto);
    }
}