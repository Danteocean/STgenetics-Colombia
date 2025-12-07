using CoreLibrary.DTOs.Orders.Response;

namespace CoreLibrary.DTOs.Orders.Requests;

public class OrderUpDto: OrderDtoRequests
{
    public Int32 orderId { get; set; }
}
