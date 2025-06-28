using FluentValidation;

namespace gizmogeo.Application.CompletedOrders.Commands.PatchNotes;

public class PatchNotesCommandValidator : AbstractValidator<PatchNotesCommand>
{
    public PatchNotesCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage("The completed Orders Id was not provided");

        RuleFor(c => c.Notes)
            .NotEmpty()
            .WithMessage("Notes are empty");
    }
}
