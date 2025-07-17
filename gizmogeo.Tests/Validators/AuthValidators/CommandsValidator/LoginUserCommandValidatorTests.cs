using FluentValidation.TestHelper;
using FluentAssertions;
using gizmogeo.Application.Auth.Commands.LoginUser;

namespace gizmogeo.Tests.Validators.AuthValidators.CommandsValidator;

public class LoginUserCommandValidatorTests
{
    private readonly LoginUserCommandValidator _validator;

    public LoginUserCommandValidatorTests()
    {
        _validator = new LoginUserCommandValidator();
    }

    [Theory]
    [InlineData("+995599123456")]
    [InlineData("+995555555555")]
    public void Should_Pass_When_Valid_PhoneNumber(string validPhone)
    {
        var command = new LoginUserCommand(validPhone);
        var result = _validator.TestValidate(command);

        result.IsValid.Should().BeTrue();
        result.ShouldNotHaveValidationErrorFor(x => x.PhoneNumber);
    }

    [Theory]
    [InlineData("995599123456")]     // missing +
    [InlineData("+1234567890")]      // wrong country code
    [InlineData("+9955123456")]      // too short
    [InlineData("+9955991234567")]   // too long
    [InlineData("+995411234567")]    // starts with 4 instead of 5
    public void Should_Fail_When_Invalid_PhoneNumber(string invalidPhone)
    {
        var command = new LoginUserCommand(invalidPhone);
        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.PhoneNumber);
    }
}
