using FluentValidation;

namespace gizmogeo.Application.Roles.Commands.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(r => r.Name)
            .Length(3, 20)
            .WithMessage("Invalid Length");
        RuleFor(r => r.Description)
           .Length(3, 50)
           .WithMessage("Invalid Length");
    }
}
