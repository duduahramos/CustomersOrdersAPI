using CustomersOrdersAPI.Context;
using CustomersOrdersAPI.Models;
using CustomersOrdersAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomersOrdersAPI.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    
    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Order> GetByIdAsync(Guid id)
    {
        var order = await _context.Order.FirstOrDefaultAsync(o => o.order_id == id);
        
        if (order == null)
        {
            return null;
        }

        return order;
    }

    public async Task<List<Order>> GetAllAsync(int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;
        
        var orders = await _context.Order
            .OrderBy(o => o.order_id)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        if (orders == null)
        {
            return null;
        }

        return orders;
    }
}