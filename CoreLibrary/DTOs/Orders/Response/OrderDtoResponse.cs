using CoreLibrary.DTOs.Extras.Response;

namespace CoreLibrary.DTOs.Orders.Response;

public class OrderDtoResponse
{
    public Int32 orderId { get; set; }

    public Int32 sandwichId { get; set; }

    public String name { get; set; }

    public List<ExtrasDtoResponse> Extras { get; set; }

    public Decimal subtotal { get; set; }

    public Decimal totalPrice { get; set; }
}