using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.DomainValidation;

public class PossibleValidNumberTest
{
    [Theory(DisplayName = "Deve validar números corretamente")]
    [InlineData("12345")]
    [InlineData("000123")]
    public void PossibleValidNumber_ShouldNotThrowException_WhenValueIsGreaterThanZeroNotNull(string validNumber)
    {
        // Act & Assert
        FluentActions.Invoking(() => Validation.DomainValidation.PossibleValidNumber(validNumber, "Número"))
            .Should().NotThrow();
    }

    [Theory(DisplayName = "Deve lançar exceção para valores inválidos")]
    [InlineData("123A", "Número deve conter apenas números.")]
    [InlineData("12-34", "Número deve conter apenas números.")]
    [InlineData("abc123", "Número deve conter apenas números.")]
    public void PossibleValidNumber_ShouldThrowException_WhenValueIsInvalid(string invalidNumber, string expectedMessage)
    {
        // Act & Assert
        var exception = Record.Exception(() => Validation.DomainValidation.PossibleValidNumber(invalidNumber, "Número"));

        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain(expectedMessage);
    }

    [Fact(DisplayName = "Deve validar números corretamente quando não requirido")]
    public void PossibleValidNumber_ShouldNotThrowException_WhenNumberIsNotRequired()
    {
        // Arrange
        string? validNumber = null;
        // Act
        var exception = Record.Exception(() => Validation.DomainValidation.PossibleValidNumber(validNumber, "Preço"));
        // Assert
        exception.Should().BeNull();
    }
}