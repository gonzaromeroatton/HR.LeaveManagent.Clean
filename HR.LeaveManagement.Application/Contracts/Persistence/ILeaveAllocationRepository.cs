using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task AddAllocations(IReadOnlyList<LeaveAllocation> allocations);
    Task<bool> AllocationExists(string userId, int leaveTypeId, int period);
    Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationWithDetails();

    Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId);
    Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
    Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationWithDetails(string userId);

}
