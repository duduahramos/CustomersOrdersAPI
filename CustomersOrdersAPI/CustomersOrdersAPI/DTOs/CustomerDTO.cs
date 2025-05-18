namespace CustomersOrdersAPI.DTOs;

public class CustomerDTO
{
    public Guid customer_id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public DateTime created_at { get; set; }
}