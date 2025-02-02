using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.DomainValidation;

public class PossibleValidCepTest
{
    [Fact]
    public void PossibleValidCep_ShouldNotThrowException_WhenValueIsValid()
    {
        // Act & Assert
        FluentActions.Invoking(() => Validation.DomainValidation.PossibleValidCep("09956300"))
            .Should().NotThrow();
    }

    [Theory(DisplayName = "Deve lançar exceção para valores inválidos")]
    [InlineData("123A", "CEP deve conter apenas números.")]
    [InlineData("12-34", "CEP deve conter apenas números.")]
    [InlineData("abc123", "CEP deve conter apenas números.")]
    [InlineData("099780012", "CEP deve conter exatamente 8 digitos.")]
    [InlineData("0780012", "CEP deve conter exatamente 8 digitos.")]
    public void PossibleValidCep_ShouldThrowException_WhenValueIsInvalid(string invalidNumber, string expectedMessage)
    {
        // Act & Assert
        var exception = Record.Exception(() => Validation.DomainValidation.PossibleValidCep(invalidNumber));

        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain(expectedMessage);
    }
}

