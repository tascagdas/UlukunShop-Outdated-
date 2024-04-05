namespace UlukunShopAPI.Domain.Entities;

public class ProductImageFile : File
{
    public bool isThumbnail { get; set; }
    public ICollection<Product> Products { get; set; }
}