using Microsoft.AspNetCore.Identity;

namespace HR.LeaveManagement.Identity.Models;

public sealed class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
