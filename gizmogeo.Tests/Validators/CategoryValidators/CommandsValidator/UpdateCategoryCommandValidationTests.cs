using FluentValidation.TestHelper;
using gizmogeo.Application.Category.Commands.UpdateCategory;

namespace gizmogeo.Tests.Validators.CategoryValidators.CommandsValidator;

public class UpdateCategoryCommandValidationTests
{
    private readonly UpdatedCategoryCommandValidator _validator;
    public UpdateCategoryCommandValidationTests()
    {
        _validator = new UpdatedCategoryCommandValidator();
    }

    [Fact]
    public void Should_Pass()
    {
        var command = new UpdateCategoryCommand
        {
            Id = 1,
            Name = "Name"
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(2, "")]
    [InlineData(2, "saaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaddddddddddddddddddddddddddddddddasdasdsadasdasd")]
    public void Should_Fail(int id, string name)
    {
        var command = new UpdateCategoryCommand { Id = id, Name = name };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}
