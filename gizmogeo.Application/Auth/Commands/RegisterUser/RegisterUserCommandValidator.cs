using FluentValidation;

namespace gizmogeo.Application.Auth.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(2, 30)
            .WithMessage("Name Length Invalid");

        //RuleFor(dto => dto.Email)
        //    .EmailAddress()
        //    .WithMessage("Invalid Email");

        RuleFor(dto => dto.PhoneNumber)
            .Matches(@"^\+9955\d{8}$")
            .WithMessage("Phone number must be in the format +9955XXXXXXXX");
            
    }
}
