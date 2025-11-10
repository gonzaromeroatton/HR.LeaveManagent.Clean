using HR.LeaveManagent.Domain.Common;

namespace HR.LeaveManagent.Domain;

public class LeaveType : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}
