using FluentValidation;

namespace gizmogeo.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(dto => dto.Id)
            .NotEmpty()
            .WithMessage("Id not Provided");
        RuleFor(dto => dto.Name)
            .Length(2, 30)
            .WithMessage("Name Length Invalid");

        RuleFor(dto => dto.PhoneNumber)
            .Matches(@"^\+9955\d{8}$")
            .WithMessage("Phone number must be in the format +9955XXXXXXXX");
    }
}
