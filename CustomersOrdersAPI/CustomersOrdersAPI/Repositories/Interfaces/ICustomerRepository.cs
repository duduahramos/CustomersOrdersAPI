using CustomersOrdersAPI.Models;

namespace CustomersOrdersAPI.Repositories.Interfaces;

public interface ICustomerRepository
{
    public Task<Customer> GetByIdAsync(Guid id);
    public Task<List<Customer>> GetAllAsync(int page, int pageSize);
}