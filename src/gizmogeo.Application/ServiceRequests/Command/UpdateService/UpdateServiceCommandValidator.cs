using FluentValidation;

namespace gizmogeo.Application.ServiceRequests.Command.UpdateService;

public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
{
    public UpdateServiceCommandValidator()
    {

        RuleFor(s => s.Id)
             .NotEmpty()
             .WithMessage("Id Not Provided");

        RuleFor(s => s.Title)
             .Length(3, 50)
             .WithMessage("Invalid Length");

        RuleFor(s => s.Description)
             .Length(3, 200)
             .WithMessage("Invalid Length");

    }
}
