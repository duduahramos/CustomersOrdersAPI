namespace CustomersOrdersAPI.DTOs;

public class PagedDTO
{
    public int PageSize;
    public int PageCount;
    public int TotalItemCount;
    public bool HasNextPage;
    public bool HasPreviousPage;
}