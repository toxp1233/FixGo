using FluentValidation;

namespace gizmogeo.Application.Auth.Commands.RecoverAccontPhoneCode;

public class RecoverAccountPhoneCodeCommandValidator : AbstractValidator<RecoverAccountPhoneCodeCommand>
{
    public RecoverAccountPhoneCodeCommandValidator()
    {
        RuleFor(dto => dto.PhoneNumber)
            .Matches(@"^\+9955\d{8}$")
            .WithMessage("Phone number must be in the format +9955XXXXXXXX");
    }
}
