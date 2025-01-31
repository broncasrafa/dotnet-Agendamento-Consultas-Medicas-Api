using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.DomainValidation;

public class IdentifierGreaterThanZeroTest
{
    [Fact]
    public void IdentifierGreaterThanZero_ShouldNotThrowException_WhenValueIsGreaterThanZero()
    {
        // Act & Assert
        var exception = Record.Exception(() => Validation.DomainValidation.IdentifierGreaterThanZero(20, "Campo"));
        // Não deve lançar exceção
        exception.Should().BeNull();
    }

    [Fact]
    public void IdentifierGreaterThanZero_ShouldThrowException_WhenValueIsNotGreaterThanZero()
    {
        // Act & Assert
        var exception = Record.Exception(() => Validation.DomainValidation.IdentifierGreaterThanZero(0, "Campo"));

        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain("Campo deve ser maior que zero.");
    }
}