using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomersOrdersAPI.Models;

[Table("customers")] //nome da tabela no banco de dados
public class Customer
{
    [Key]
    public Guid customer_id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public DateTime created_at { get; set; }
}