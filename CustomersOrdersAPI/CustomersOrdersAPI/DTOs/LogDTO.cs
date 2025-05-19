namespace CustomersOrdersAPI.DTOs;

public class LogDTO<T>
{
    public DateTime Timestamp { get; set; }
    public string Level { get; set; }
    public string Message { get; set; }
    public int ExecutionTimeMs { get; set; }
    public int QuantityFound { get; set; }
    public string UserId { get; set; }
    public List<T> Data { get; set; }
}

// {
//     "timestamp": "2025-05-17T20:15:01Z",
//     "level": "INFO",
//     "message": "Query executed for customer 42",
//     "sqlQuery": "SELECT * FROM orders WHERE customer_id = 42",
//     "executionTimeMs": 15,
//     "userId": "admin-api"
// }