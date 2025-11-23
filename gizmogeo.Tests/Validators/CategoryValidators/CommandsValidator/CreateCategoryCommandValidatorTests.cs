using FluentAssertions;
using FluentValidation.TestHelper;
using gizmogeo.Application.Category.Commands.CreateCategory;
using Xunit;
namespace gizmogeo.Tests.Validators.CategoryValidators.CommandsValidator;

public class CreateCategoryCommandValidatorTests
{
    private readonly CreateCategoryCommandValidatior _validator;
    public CreateCategoryCommandValidatorTests()
    {
        _validator = new CreateCategoryCommandValidatior();
    }

    [Fact]
    public void Should_Pass()
    {
        var Command = new CreateCategoryCommand("name");
        var result = _validator.TestValidate(Command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("")]
    [InlineData("sh")]
    [InlineData("LOOOOOOOOOOOOOOOOOOOOOOOOOOOONG")]
    public void Should_Fail_If_Name_Too_Short_Or_Long(string name)
    {
        var command = new CreateCategoryCommand(name);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}
