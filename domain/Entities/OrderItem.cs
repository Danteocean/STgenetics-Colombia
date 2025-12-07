using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("order_item")]
public class OrderItem
{
    [Key]
    [Column("order_item_id")]
    public int orderItemId { get; set; }

    [Column("order_id")]
    public int orderId { get; set; }

    [Column("extra_id")]
    public int extraId { get; set; }

    [Column("created_date")]
    public DateTime createdDate { get; set; }

    [Column("updated_date")]
    public DateTime updatedDate { get; set; }
}