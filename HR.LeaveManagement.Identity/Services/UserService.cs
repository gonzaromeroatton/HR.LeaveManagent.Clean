using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace HR.LeaveManagement.Identity.Services;

/// <summary>
/// 
/// </summary>
public sealed class UserService : IUserService
{
    /// <summary>
    /// 
    /// </summary>
    private readonly UserManager<ApplicationUser> _userManager;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userManager"></param>
    public UserService(UserManager<ApplicationUser> userManager)
    {
        this._userManager = userManager;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<Employee> GetEmployee(string userId)
    {
        var employee = await this._userManager.FindByIdAsync(userId);

        return new Employee
        {
            Id = employee.Id,
            Email = employee.Email,
            FirstName = employee.FirstName,
            LastName = employee.LastName
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IReadOnlyList<Employee>> GetEmployees()
    {
        var employees = this._userManager.Users.Select(user => new Employee
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName
        }).ToList();

        return Task.FromResult<IReadOnlyList<Employee>>(employees);
    }
}
