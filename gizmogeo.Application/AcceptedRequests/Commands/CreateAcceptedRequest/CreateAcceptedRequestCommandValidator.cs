using FluentValidation;

namespace gizmogeo.Application.AcceptedRequests.Commands.CreateAcceptedRequest;

public class CreateAcceptedRequestCommandValidator : AbstractValidator<CreateAcceptedRequestCommand>
{
    public CreateAcceptedRequestCommandValidator()
    {
        RuleFor(a => a.ServiceRequestId)
            .NotEmpty()
            .WithMessage("Service Request ID not provided");

        RuleFor(a => a.Message)
            .NotEmpty()
            .WithMessage("Empty Message");

        RuleFor(a => a.EstimatedCost)
            .NotNull()
            .WithMessage("Estimated cost must be provided");
    }
}
