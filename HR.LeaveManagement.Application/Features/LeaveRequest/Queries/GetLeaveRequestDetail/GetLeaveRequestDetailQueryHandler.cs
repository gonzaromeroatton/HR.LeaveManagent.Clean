using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public class GetLeaveRequestDetailQueryHandler : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDto>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;

    public GetLeaveRequestDetailQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
    {
        this._leaveRequestRepository = leaveRequestRepository;
        this._mapper = mapper;
    }

    public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
    {
        var leaveRequest = this._mapper.Map<LeaveRequestDetailsDto>(await this._leaveRequestRepository.GetLeaveRequestWithDetails(request.Id));

        // Add employee details as needed.
        
        return leaveRequest;
    }
}
