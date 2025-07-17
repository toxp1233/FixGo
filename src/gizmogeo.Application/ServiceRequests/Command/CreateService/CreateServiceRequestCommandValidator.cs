using FluentValidation;

namespace gizmogeo.Application.ServiceRequests.Command.CreateService;

public class CreateServiceRequestCommandValidator : AbstractValidator<CreateServiceRequestCommand>
{
    public CreateServiceRequestCommandValidator()
    {
        RuleFor(s => s.Title)
             .Length(3, 50)
             .WithMessage("Invalid Length");
        RuleFor(s => s.Description)
             .Length(3, 200)
             .WithMessage("Invalid Length");
        RuleFor(s => s.CategoryId)
             .NotEmpty()
             .WithMessage("Category Not Provided");
    }
}
