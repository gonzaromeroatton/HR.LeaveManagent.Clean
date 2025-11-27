using HR.LeaveManagement.Application.Models.Identity;

namespace HR.LeaveManagement.Application.Contracts.Identity;

public interface IUserService
{
    Task<IReadOnlyList<Employee>> GetEmployees();

    Task<Employee> GetEmployee(string userId);
}
