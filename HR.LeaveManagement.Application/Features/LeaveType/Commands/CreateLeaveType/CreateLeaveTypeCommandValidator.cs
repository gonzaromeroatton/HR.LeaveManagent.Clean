using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

        RuleFor(p => p.DefaultDays)
            .LessThan(100).WithMessage("{PropertyName} must not exceed 100.")
            .GreaterThan(1).WithMessage("{PropertyName} must be greater than 1.");

        RuleFor(p => p)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("Leave type with the same name already exists.");

        this._leaveTypeRepository = leaveTypeRepository;
    }

    private Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken token = default)
    {
        return this._leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }
}
