using FluentValidation;
namespace gizmogeo.Application.ServiceRequests.Command.DeleteService;

public class DeleteServiceCommandValidator : AbstractValidator<DeleteServiceCommand>
{
    public DeleteServiceCommandValidator()
    {
        RuleFor(s => s.Id)
             .NotEmpty()
             .WithMessage("Id Not Provided");

    }
}
