namespace CustomersOrdersAPI.Repositories.Interfaces;

public interface IUnitOfWork
{
    public ICustomerRepository CustomerRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public Task CommitAsync();
    public void Dispose();
}