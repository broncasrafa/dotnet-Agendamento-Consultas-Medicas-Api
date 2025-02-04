using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ConfirmEmail;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Account.Command.ConfirmEmail;

public class ConfirmEmailRequestValidatorTest
{
    private readonly ConfirmEmailRequestValidator _validator;

    public ConfirmEmailRequestValidatorTest()
    {
        _validator = new ConfirmEmailRequestValidator();
    }

    [Fact]
    public void Validate_ShouldBeValid_When_InputIsValid()
    {
        var request = new ConfirmEmailRequest(Guid.NewGuid().ToString(), "code");
        var result = _validator.Validate(request);
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldHaveErrors_When_InputUserIdIsInvalid(string UserId)
    {
        // Arrange
        var request = new ConfirmEmailRequest(UserId, "qUE4IUU2jy");
        // Act
        var result = _validator.TestValidate(request);
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.ShouldHaveValidationErrorFor(c => c.UserId).WithErrorMessage("O Id do usuário é obrigatório, não deve ser nulo ou vazio");
        result.ShouldNotHaveValidationErrorFor(c => c.Code);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldHaveErrors_When_InputCodeIsInvalid(string Code)
    {
        // Arrange
        var request = new ConfirmEmailRequest("7009b7bc-c253-4998-b72a-75408186edf6", Code);
        // Act
        var result = _validator.TestValidate(request);
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.ShouldHaveValidationErrorFor(c => c.Code).WithErrorMessage("O Código de confirmação é obrigatório, não deve ser nulo ou vazio");
        result.ShouldNotHaveValidationErrorFor(c => c.UserId);
    }
}