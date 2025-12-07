using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;


[Table("extra")]
public class Extras
{
    [Key]

    public Int32 extraId { get; set; }

    public int categoryId { get; set; }


    public String name { get; set; }

    public Decimal price { get; set; }

    public Boolean isActive { get; set; }


    public DateTime createdDate { get; set; }


    public DateTime updatedDate { get; set; }

}