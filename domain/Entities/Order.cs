using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("order")]
public class Order
{
    [Key]
    public Int32 orderId { get; set; }

    public Int32 sandwichId { get; set; }

    public Int32 discountRuleId { get; set; }

    public Decimal subtotal { get; set; }

    public Decimal totalPrice { get; set; }

    public Boolean isActive { get; set; }

    public DateTime createdDate { get; set; }

    public DateTime updatedDate { get; set; }

}