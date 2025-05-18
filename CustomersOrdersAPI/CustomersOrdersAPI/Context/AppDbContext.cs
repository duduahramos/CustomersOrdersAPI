using CustomersOrdersAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomersOrdersAPI.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Customer>? Customer { get; set; }
    public DbSet<Order>? Order { get; set; }
}