using FluentValidation;

namespace gizmogeo.Application.AcceptedRequests.Commands.UpdateAcceptedRequest
{
    public class UpdateAcceptedRequestCommandValidator : AbstractValidator<UpdateAcceptedRequestCommand>
    {
        public UpdateAcceptedRequestCommandValidator()
        {
            RuleFor(a => a.Message)
                .NotEmpty()
                .WithMessage("Empty Message");

            RuleFor(a => a.EstimatedCost)
                .NotNull()
                .WithMessage("Estimated cost must be provided");

            RuleFor(a => a.Status)
                .IsInEnum()
                .WithMessage("Invalid request status");
        }
    }
}
