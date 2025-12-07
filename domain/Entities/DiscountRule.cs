using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("discount_rule")]
public class DiscountRule
{
    [Key]
    public Int32 discountRuleId { get; set; }

    public String name { get; set; }

    public Int32 discountPercent { get; set; }

    public Boolean isActive { get; set; }

    public DateTime createdDate { get; set; }

    public DateTime updatedDate { get; set; }
}