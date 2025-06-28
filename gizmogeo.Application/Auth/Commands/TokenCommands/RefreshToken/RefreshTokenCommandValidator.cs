using FluentValidation;

namespace gizmogeo.Application.Auth.Commands.TokenCommands.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(t => t.UserId)
            .NotEmpty()
            .WithMessage("User Id is not given");
        RuleFor(t => t.RefreshToken)
            .NotEmpty()
            .WithMessage("RefreshTokenEmpty");

    }
}
