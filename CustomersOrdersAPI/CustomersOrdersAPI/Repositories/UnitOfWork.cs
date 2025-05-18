using CustomersOrdersAPI.Context;
using CustomersOrdersAPI.Repositories.Interfaces;

namespace CustomersOrdersAPI.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public AppDbContext _context;
    private ICustomerRepository _customerRepository;
    private IOrderRepository _orderRepository;

    public ICustomerRepository CustomerRepository
    {
        get
        {
            return _customerRepository ?? new CustomerRepository(_context);
        }
    }

    public IOrderRepository OrderRepository
    {
        get
        {
            return _orderRepository ?? new OrderRepository(_context);
        }
    }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task CommitAsync()
    {
        _context.SaveChangesAsync();
    }

    // libera os recursos não gerenciados
    public void Dispose()
    {
        _context.Dispose();
    }
}

internal interface ICategoriaRepository
{
}

internal interface IProdutoRepository
{
}