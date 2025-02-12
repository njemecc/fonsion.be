using Microsoft.AspNetCore.Identity;

namespace Fonsion.be.Domain.Users;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string ClerkId { get; set; }
    
}