using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}

public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
{

}

public interface ILeaveTAllocationRepository : IGenericRepository<LeaveAllocation>
{

}

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{

}
