namespace CustomersOrdersAPI.DTOs;

public class OrderDTO
{
    public Guid order_id { get; set; }
    public Guid customer_id { get; set; }
    public string product_name { get; set; }
    public int quantity { get; set; }
    public decimal total_value { get; set; }
    public DateTime created_at { get; set; }
}