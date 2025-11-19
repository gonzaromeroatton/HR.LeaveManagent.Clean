using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public UpdateLeaveAllocationCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
    {
        this._mapper = mapper;
        this._leaveTypeRepository = leaveTypeRepository;
        this._leaveAllocationRepository = leaveAllocationRepository;
    }

    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveAllocationCommandValidator(this._leaveTypeRepository, this._leaveAllocationRepository);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid leave Allocation", validationResult);

        var leaveAllocation = await this._leaveAllocationRepository.GetByIdAsync(request.Id);

        if (leaveAllocation is null)
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);

        this._mapper.Map(request, leaveAllocation);

        await this._leaveAllocationRepository.UpdateAsync(leaveAllocation);

        return Unit.Value;
    }
}
