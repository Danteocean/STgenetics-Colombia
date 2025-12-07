namespace Domain.Entities;

public class ExtrasById
{
    public Int32 extraId { get; set; }

    public int categoryId { get; set; }


    public String nameExt { get; set; }

    public Decimal priceExt { get; set; }

    public Boolean isActive { get; set; }


    public DateTime createdDate { get; set; }


    public DateTime updatedDate { get; set; }
}