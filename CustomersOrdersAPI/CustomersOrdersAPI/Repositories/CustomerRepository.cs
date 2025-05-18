using CustomersOrdersAPI.Context;
using CustomersOrdersAPI.Models;
using CustomersOrdersAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomersOrdersAPI.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Customer> GetByIdAsync(Guid id)
    {
        var customer = await _context.Customer.FirstOrDefaultAsync(c => c.customer_id == id);
        
        if (customer == null)
        {
            return null;
        }
        
        return customer;
    }

    public async Task<List<Customer>> GetAllAsync(int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;
        
        var customers = await _context.Customer
            .OrderBy(c => c.customer_id)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        if (customers == null)
        {
            return null;
        }

        return customers;
    }
}