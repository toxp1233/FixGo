using FluentValidation;

namespace gizmogeo.Application.Category.Commands.CreateCategory;

public class CreateCategoryCommandValidatior : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidatior()
    {
        RuleFor(c => c.Name)
            .Length(3, 20)
            .WithMessage("Invalid Length");
    }
}
