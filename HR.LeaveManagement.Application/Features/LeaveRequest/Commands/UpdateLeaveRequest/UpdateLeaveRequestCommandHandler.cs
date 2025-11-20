using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _appLogger;

    public UpdateLeaveRequestCommandHandler(IMapper mapper, IEmailSender emailSender, 
        ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository, 
        IAppLogger<UpdateLeaveRequestCommandHandler> appLogger)
    {
        this._mapper = mapper;
        this._emailSender = emailSender;
        this._leaveRequestRepository = leaveRequestRepository;
        this._leaveTypeRepository = leaveTypeRepository;
        this._appLogger = appLogger;
    }

    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await this._leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
            throw new NotFoundException(nameof(LeaveRequest), request.Id);

        var validator = new UpdateLeaveRequestCommandValidator(this._leaveTypeRepository, this._leaveRequestRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid Leave request", validationResult);

        this._mapper.Map(request, leaveRequest);

        await this._leaveRequestRepository.UpdateAsync(leaveRequest);

        try
        {
            // Send confirmation email
            var email = new EmailMessage
            {
                To = string.Empty, //TODO: Get requestor email address from employee record
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} has been updated successfully.",
                Subject = "Leave Request Submitted"
            };

            await this._emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            // Log or handle error, but don't throw as the main operation was successful
            this._appLogger.LogWarning(ex.Message);
            throw;
        }

        return Unit.Value;

    }
}
