using Microsoft.AspNetCore.Identity;

namespace WebCommerce.DataAccess;

public class WebCommerceUserIdentity : IdentityUser
{
    public string Name { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public DateTime BirthDate { get; set; }
}