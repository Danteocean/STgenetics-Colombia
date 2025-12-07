using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Sandwich")]
public class Sandwich
{
    [Key]
    public Int32 sandwichId { get; set; }

    public String name { get; set; }

    public Decimal price { get; set; }

    public Boolean isActive { get; set; }
}
