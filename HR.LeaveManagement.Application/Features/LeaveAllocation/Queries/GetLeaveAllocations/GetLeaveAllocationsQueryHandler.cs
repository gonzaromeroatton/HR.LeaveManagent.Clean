using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

internal class GetLeaveAllocationsQueryHandler : IRequestHandler<GetLeaveAllocationsQuery, IReadOnlyList<LeaveAllocationDto>>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;

    public GetLeaveAllocationsQueryHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
    {
        this._leaveAllocationRepository = leaveAllocationRepository;
        this._mapper = mapper;
    }

    public async Task<IReadOnlyList<LeaveAllocationDto>> Handle(GetLeaveAllocationsQuery request, CancellationToken cancellationToken)
    {
        // Get records for specific user
        // Get allocations for employee

        var leaveAllocations = await this._leaveAllocationRepository.GetLeaveAllocationWithDetails();
        var allocations = _mapper.Map<IReadOnlyList<LeaveAllocationDto>>(leaveAllocations);

        return allocations;
    }
}
