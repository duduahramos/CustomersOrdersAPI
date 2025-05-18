using CustomersOrdersAPI.Models;

namespace CustomersOrdersAPI.Repositories.Interfaces;

public interface IOrderRepository
{
    public Task<Order> GetByIdAsync(Guid id);
    public Task<List<Order>> GetAllAsync(int page, int pageSize);
}