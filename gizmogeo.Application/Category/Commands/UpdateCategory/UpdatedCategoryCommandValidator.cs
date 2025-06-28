using FluentValidation;

namespace gizmogeo.Application.Category.Commands.UpdateCategory;

public class UpdatedCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdatedCategoryCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull()
            .WithMessage("Id was not provided");
        RuleFor(c => c.Name)
            .Length(3, 20)
            .WithMessage("Invalid Length");
    }
}
