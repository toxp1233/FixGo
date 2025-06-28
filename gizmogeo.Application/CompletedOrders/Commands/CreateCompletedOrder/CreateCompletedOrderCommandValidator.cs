using FluentValidation;

namespace gizmogeo.Application.CompletedOrders.Commands.CreateCompletedOrder;

public class CreateCompletedOrderCommandValidator : AbstractValidator<CreateCompletedOrderCommand>
{
    public CreateCompletedOrderCommandValidator()
    {
        RuleFor(x => x.AcceptedRequestId)
            .NotEmpty()
            .WithMessage("AcceptedRequestId is required.");

        RuleFor(x => x.FinalCost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Final cost cannot be negative.");
    }
}
