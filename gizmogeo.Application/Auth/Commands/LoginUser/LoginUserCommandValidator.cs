using FluentValidation;

namespace gizmogeo.Application.Auth.Commands.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(dto => dto.PhoneNumber)
            .Matches(@"^\+9955\d{8}$")
            .WithMessage("Phone number must be in the format +9955XXXXXXXX");
    }
}
