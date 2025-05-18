using CustomersOrdersAPI.DTOs;
using CustomersOrdersAPI.Models;

namespace CustomersOrdersAPI.Mappings;

public static class OrderMapping
{
    public static OrderDTO ToDTO(this Order orders)
    {
        return new OrderDTO
        {
            order_id = orders.order_id,
            customer_id = orders.customer_id,
            product_name = orders.product_name,
            quantity = orders.quantity,
            total_value = orders.total_value,
            created_at = orders.created_at
        };
    }

    public static List<OrderDTO> ToDTOs(this List<Order> orders)
    {
        return orders.Select(o => o.ToDTO()).ToList();
    }
}