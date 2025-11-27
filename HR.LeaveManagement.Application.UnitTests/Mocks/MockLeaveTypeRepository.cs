using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;

namespace HR.LeaveManagement.Application.UnitTests.Mocks;

public class MockLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> GetLeaveTypeMockLeaveTypesRepository()
    {
        var leaveTypes = new List<LeaveType>
        {
            new() {
                Id = 1,
                DefaultDays = 10,
                Name = "Test Vacation"
            },
            new()
            {
                Id = 2,
                DefaultDays = 15,
                Name = "Test Sick"
            },
            new()
            {
                Id = 3,
                DefaultDays = 15,
                Name = "Test Maternity"
            }
        };

        var mockRepo = new Mock<ILeaveTypeRepository>();

        // Give me all leave types
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(leaveTypes);

        // Create a new mocked leave type
        mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>())).Returns((LeaveType leaveType) =>
        {
            leaveTypes.Add(leaveType);
            return Task.CompletedTask;
        });

        return mockRepo;
    }
}
