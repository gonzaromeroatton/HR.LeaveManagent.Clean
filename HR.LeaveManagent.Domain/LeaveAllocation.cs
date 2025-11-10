using HR.LeaveManagent.Domain.Common;

namespace HR.LeaveManagent.Domain;

public class LeaveAllocation : BaseEntity
{
    public int NumberOfDays { get; set; }
    public LeaveType? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public int Period { get; set; }
}