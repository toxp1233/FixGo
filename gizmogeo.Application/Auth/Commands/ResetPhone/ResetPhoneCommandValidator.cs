using FluentValidation;

namespace gizmogeo.Application.Auth.Commands.ResetPhone;

public class ResetPhoneCommandValidator : AbstractValidator<ResetPhoneCommand>
{
    public ResetPhoneCommandValidator()
    {
        RuleFor(dto => dto.OldNumber)
            .NotEmpty()
            .Matches(@"^\+9955\d{8}$")
            .WithMessage("Phone number must be in the format +9955XXXXXXXX");

        RuleFor(dto => dto.NewNumber)
            .NotEmpty()
            .Matches(@"^\+9955\d{8}$")
            .WithMessage("Phone number must be in the format +9955XXXXXXXX");

        RuleFor(dto => dto.RecoveryCode)
            .NotEmpty()
            .Length(6)
            .Matches(@"^\d{6}$") // ensures exactly 6 digits
            .WithMessage("Recovery Code must be exactly 6 numeric digits");

    }
}
