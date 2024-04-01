using Microsoft.AspNetCore.Identity;

namespace UlukunShopAPI.Domain.Entities.Identity;

public class AppUser : IdentityUser<string>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}