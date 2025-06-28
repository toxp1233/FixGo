using FluentValidation;

namespace gizmogeo.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(dto => dto.Name)
             .Length(2, 30)
             .WithMessage("Name Length Invalid");

        RuleFor(dto => dto.PhoneNumber)
            .Matches(@"^\+9955\d{8}$")
            .WithMessage("Phone number must be in the format +9955XXXXXXXX");
    }
}
