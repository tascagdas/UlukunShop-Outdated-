using Microsoft.AspNetCore.Identity;

namespace UlukunShopAPI.Domain.Entities.Identity;

public class AppUser : IdentityUser<string>
{
    public string firstName { get; set; }
    public string lastName { get; set; }
}