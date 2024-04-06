using Microsoft.AspNetCore.Identity;

namespace UlukunShopAPI.Domain.Entities.Identity;

public class AppUser : IdentityUser<string>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenEndDate { get; set; }
    public ICollection<ShoppingCart> ShoppingCarts { get; set; }
}