using FluentValidation.TestHelper;
using gizmogeo.Application.Auth.Commands.RegisterUser;
using Xunit;

namespace gizmogeo.Tests.Validators.AuthValidators.CommandsValidator;

public class RegisterUserCommandValidatorTests
{
    private readonly RegisterUserCommandValidator _validator;

    public RegisterUserCommandValidatorTests()
    {
        _validator = new RegisterUserCommandValidator();
    }

    [Fact]
    public void Should_Pass_When_Valid_Input()
    {
        var command = new RegisterUserCommand
        {
            Name = "Soso",
            PhoneNumber = "+995599123456"
        };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("")]
    [InlineData("S")]
    [InlineData("A very very very very long name that is too long")]
    public void Should_Fail_When_Name_Invalid(string invalidName)
    {
        var command = new RegisterUserCommand
        {
            Name = invalidName,
            PhoneNumber = "+995599123456"
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Theory]
    [InlineData("995599123456")]     // missing +
    [InlineData("+1234567890")]      // wrong country code
    [InlineData("+9955123456")]      // too short
    [InlineData("+9955991234567")]   // too long
    [InlineData("+995411234567")]    // starts with 4 instead of 5
    public void Should_Fail_When_Invalid_PhoneNumber(string invalidPhone)
    {
        var command = new RegisterUserCommand
        {
            Name = "ValidName",
            PhoneNumber = invalidPhone
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.PhoneNumber);
    }
}
