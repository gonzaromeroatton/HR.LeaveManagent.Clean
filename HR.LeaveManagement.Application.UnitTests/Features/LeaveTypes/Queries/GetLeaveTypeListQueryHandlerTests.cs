using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Shouldly;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries;

public class GetLeaveTypeListQueryHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private IMapper _mapper;
    private Mock<IAppLogger<GetLeaveTypesQueryHandler>> _mockAppLogger;

    public GetLeaveTypeListQueryHandlerTests()
    {
        this._mockRepo = MockLeaveTypeRepository.GetLeaveTypeMockLeaveTypesRepository();
       
        var mapperConfig = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile<LeaveTypeProfile>();
            }, new NullLoggerFactory() // Required as of 15.x.x onwards.
        );

        this._mapper = mapperConfig.CreateMapper();
        this._mockAppLogger = new Mock<IAppLogger<GetLeaveTypesQueryHandler>>();
    }

    /// <summary>
    /// Tests the <see cref="GetLeaveTypesQueryHandler"/> to ensure it retrieves a list of leave types.
    /// </summary>
    /// <remarks>This test verifies that the handler correctly processes the <see cref="GetLeaveTypesQuery"/> 
    /// and returns a list of leave types as a <see cref="List{T}"/> of <see cref="LeaveTypeDto"/>.     It also ensures
    /// that the returned list implements <see cref="IReadOnlyList{T}"/> and contains the expected number of
    /// items.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task GetLeaveTypeListTest()
    {
        var handler = new GetLeaveTypesQueryHandler(this._mapper, this._mockRepo.Object, this._mockAppLogger.Object);

        var result = await handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);

        result.ShouldBeAssignableTo<IReadOnlyList<LeaveTypeDto>>(); // AutoMapper checks for concrete types at runtime, ShouldBeOfType some interface will fail always.
        result.ShouldBeOfType<List<LeaveTypeDto>>(); // Checks for concrete type at rubntime. Interface (IreadoNlyList) won't work here.
        result.Count.ShouldBe(3);
    }
}
