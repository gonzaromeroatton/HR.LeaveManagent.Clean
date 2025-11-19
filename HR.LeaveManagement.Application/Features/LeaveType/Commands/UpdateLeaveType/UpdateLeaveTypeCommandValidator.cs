using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveTypeMustExists);

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters.");

        RuleFor(p => p.DefaultDays)
            .GreaterThan(1).WithMessage("{PropertyName} cannot exceed 100.")
            .LessThan(100).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

        RuleFor(q => q)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("Leave type already exists.");

        this._leaveTypeRepository = leaveTypeRepository;
    }

    private async Task<bool> LeaveTypeMustExists(int id, CancellationToken cancellationToken = default)
    {
        var leaveType = await this._leaveTypeRepository.GetByIdAsync(id);

        return leaveType != null;
    }

    private async Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand command, CancellationToken cancellationToken = default)
    {
        return await this._leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }
}
