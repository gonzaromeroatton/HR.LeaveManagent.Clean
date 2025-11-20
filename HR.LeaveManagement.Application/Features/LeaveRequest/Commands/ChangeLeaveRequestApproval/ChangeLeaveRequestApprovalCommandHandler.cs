using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public ChangeLeaveRequestApprovalCommandHandler(IMapper mapper, IEmailSender emailSender,
        ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository)
    {
        this._mapper = mapper;
        this._emailSender = emailSender;
        this._leaveRequestRepository = leaveRequestRepository;
        this._leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await this._leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest == null)
            throw new NotFoundException(nameof(LeaveRequest), request.Id);

        leaveRequest.Approved = request.Approved;
        await this._leaveRequestRepository.UpdateAsync(leaveRequest);

        // If request is approved, get and update the employee's allocations

        // Send confirmation email
        var email = new EmailMessage
        {
            To = string.Empty, // Get email from employee record
            Body = $"The approval status for your leave request for {leaveRequest.StartDate:D} " +
                    $"to {leaveRequest.EndDate:D} has beenn updated",
            Subject = "Leave Request Approval Status Updated"
        };

        await this._emailSender.SendEmail(email);

        return Unit.Value;
    }
}
