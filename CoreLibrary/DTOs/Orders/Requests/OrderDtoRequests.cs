namespace CoreLibrary.DTOs.Orders.Requests;

public class OrderDtoRequests
{
    public int sandwichId { get; set; }
    public List<Int32> ExtraIds { get; set; } = new();
}