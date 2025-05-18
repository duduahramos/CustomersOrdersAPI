using CustomersOrdersAPI.DTOs;
using CustomersOrdersAPI.Models;

namespace CustomersOrdersAPI.Mappings;

public static class CustomerMapping
{
    public static CustomerDTO ToDTO(this Customer customers)
    {
        return new CustomerDTO
        {
            customer_id = customers.customer_id,
            name = customers.name,
            email = customers.email,
            created_at = customers.created_at
        };
    }

    public static List<CustomerDTO> ToDTOs(this List<Customer> customers)
    {
        return customers.Select(o => o.ToDTO()).ToList();
    }
}