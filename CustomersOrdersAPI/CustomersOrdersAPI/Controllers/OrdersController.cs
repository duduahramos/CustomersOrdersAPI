using System.Text.Json;
using CustomersOrdersAPI.DTOs;
using CustomersOrdersAPI.Mappings;
using CustomersOrdersAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomersOrdersAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IUnitOfWork unitOfWork, ILogger<OrdersController> logger)
    {
        _logger = logger;
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
        
        _logger.LogInformation(JsonSerializer.Serialize(new LogDTO<OrderDTO>
        {
            Timestamp = DateTime.UtcNow,
            Level = "INFO",
            Message = "Orders retrieved successfully.",
            ExecutionTimeMs = 15,
            QuantityFound = ordersDto.Count,
            UserId = "admin-api",
            Data = ordersDto
        }));

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
        
        _logger.LogInformation(JsonSerializer.Serialize(new LogDTO<OrderDTO>
        {
            Timestamp = DateTime.UtcNow,
            Level = "INFO",
            Message = $"Order '{id}' retrieved successfully.",
            ExecutionTimeMs = 15,
            QuantityFound = 1,
            UserId = "admin-api",
            Data = new List<OrderDTO> { orderDto }
        }));
        
        return Ok(orderDto);
    }
}