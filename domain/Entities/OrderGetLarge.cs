namespace Domain.Entities;

public class OrderGetLarge
{
    public Int32 orderId { get; set; }
    public Int32 sandwichId { get; set; }
    public String name { get; set; }

    public Decimal subtotal { get; set; }
    public Decimal totalPrice { get; set; }

    public List<ExtrasDtoResponse> Extras { get; set; }
}

public class ExtrasDtoResponse
{
    public Int32 extraId { get; set; }

    public String nameExt { get; set; }

    public Decimal priceExt { get; set; }
}

public class OrderRaw
{
    public Int32 orderId { get; set; }
    public Int32 sandwichId { get; set; }
    public String name { get; set; }

    public Int32? extraId { get; set; }     
    public String? nameExt { get; set; }  
    public Decimal? priceExt { get; set; } 

    public Decimal subtotal { get; set; }
    public Decimal totalPrice { get; set; }
}

