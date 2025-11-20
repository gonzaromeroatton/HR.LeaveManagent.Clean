using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, IReadOnlyList<LeaveTypeDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<GetLeaveTypesQueryHandler> _logger;

    public GetLeaveTypesQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IAppLogger<GetLeaveTypesQueryHandler> logger)
    {
        this._mapper = mapper;
        this._leaveTypeRepository = leaveTypeRepository;
        this._logger = logger;
    }

    public async Task<IReadOnlyList<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken = default)
    {
        // Query the database
        var leaveTypes =  await _leaveTypeRepository.GetAllAsync();

        // Convert data objects to DTO
        var data = _mapper.Map<IReadOnlyList<LeaveTypeDto>>(leaveTypes);

        // Return list of DTO objects
        this._logger.LogInformation("Leave Types were retrieved successfully");
        return data;
    }
}
 