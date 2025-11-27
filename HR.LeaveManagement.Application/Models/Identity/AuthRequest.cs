namespace HR.LeaveManagement.Application.Models.Identity;

public sealed class AuthRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
