using FluentValidation;

namespace gizmogeo.Application.Roles.Commands.UpdateRole;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(r => r.Name)
            .Length(3, 20)
            .WithMessage("Invalid Length");
        RuleFor(r => r.Description)
           .Length(3, 50)
           .WithMessage("Invalid Length");
    }
}
