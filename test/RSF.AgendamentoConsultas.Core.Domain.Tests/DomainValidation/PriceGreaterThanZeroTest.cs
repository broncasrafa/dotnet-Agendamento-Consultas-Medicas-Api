using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.DomainValidation;

public class PriceGreaterThanZeroTest
{
    [Fact]
    public void PriceGreaterThanZero_ShouldNotThrowException_WhenValueIsGreaterThanZero()
    {
        // Arrange
        decimal validPrice = 10.50m;
        // Act
        var exception = Record.Exception(() => Validation.DomainValidation.PriceGreaterThanZero(validPrice, "Preço"));
        // Assert
        exception.Should().BeNull();
    }

    [Theory]
    [InlineData(0, "Preço deve ser maior que zero.")]
    [InlineData(-10.50, "Preço deve ser maior que zero.")]
    public void PriceGreaterThanZero_ShouldThrowException_WhenValueIsNotGreaterThanZero(decimal value, string exceptionMessage)
    {
        // Act & Assert
        var exception = Record.Exception(() => Validation.DomainValidation.PriceGreaterThanZero(value, "Preço"));

        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain(exceptionMessage);
    }

    [Fact]
    public void PriceGreaterThanZero_ShouldNotThrowException_WhenPriceIsNotRequired()
    {
        // Arrange
        decimal? validPrice = null;
        // Act
        var exception = Record.Exception(() => Validation.DomainValidation.PriceGreaterThanZero(validPrice, "Preço"));
        // Assert
        exception.Should().BeNull();
    }
}