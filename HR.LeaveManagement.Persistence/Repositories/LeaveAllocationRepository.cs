using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(HrDatabaseContext context) : base(context)
    {

    }

    public async Task AddAllocations(IReadOnlyList<LeaveAllocation> allocations)
    {
        await this._context.AddRangeAsync(allocations);
        await this._context.SaveChangesAsync();
    }

    public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
    {
        return await this._context.LeaveAllocations.AnyAsync(q =>
            q.EmployeeId == userId &&
            q.LeaveTypeId == leaveTypeId &&
            q.Period == period);
    }

    public async Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationWithDetails()
    {
        var leaveAllocations = await this._context.LeaveAllocations
            .Include(q => q.LeaveType)
            .ToListAsync();

        return leaveAllocations;
    }

    public async Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationWithDetails(string userId)
    {
        var leaveAllocations = await this._context.LeaveAllocations.Where(q => q.EmployeeId == userId)
            .Include(q => q.LeaveType)
            .ToListAsync();

        return leaveAllocations;
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
    {
        var leaveAllocations = await this._context.LeaveAllocations
            .Include(q => q.LeaveType)
            .FirstOrDefaultAsync(q => q.Id == id);

        return leaveAllocations;
    }

    public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
    {
        return await this._context.LeaveAllocations
            .FirstOrDefaultAsync(q => q.EmployeeId == userId && q.LeaveTypeId == leaveTypeId);
    }
}
