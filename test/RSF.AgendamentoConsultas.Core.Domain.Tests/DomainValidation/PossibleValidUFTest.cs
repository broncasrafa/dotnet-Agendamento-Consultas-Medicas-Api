using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.DomainValidation;

public class PossibleValidUFTest
{
    [Theory(DisplayName = "Deve validar UF corretamente")]
    [InlineData("SP")]
    [InlineData("MG")]
    public void PossibleValidUF_ShouldNotThrowException_WhenUfIsValid(string value)
    {
        // Act & Assert
        FluentActions.Invoking(() => Validation.DomainValidation.PossibleValidUF(value, "UF"))
            .Should().NotThrow();
    }

    [Fact(DisplayName = "Deve validar UF corretamente quando não for nulo")]
    public void PossibleValidUF_ShouldNotThrowException_WhenUfIsNotRequired()
    {
        // Act & Assert
        FluentActions.Invoking(() => Validation.DomainValidation.PossibleValidUF(null, "UF"))
            .Should().NotThrow();
    }

    [Theory(DisplayName = "Deve lançar exceção para valores inválidos")]
    [InlineData("", "UF deve ter 2 caracteres.")]
    [InlineData("S", "UF deve ter 2 caracteres.")]
    [InlineData("SSP", "UF deve ter 2 caracteres.")]
    [InlineData("sp", "UF deve conter apenas letras maiúsculas.")]
    [InlineData("Sp", "UF deve conter apenas letras maiúsculas.")]
    [InlineData("sP", "UF deve conter apenas letras maiúsculas.")]
    public void PossibleValidUF_ShouldThrowException_WhenUfIsInvalid(string value, string expectedMessage)
    {
        // Act & Assert
        FluentActions.Invoking(() => Validation.DomainValidation.PossibleValidUF(value, "UF"))
            .Should().Throw<EntityValidationException>()
            .WithMessage(expectedMessage);
    }

    [Fact(DisplayName = "Deve lançar exceção para valores inválidos")]
    public void PossibleValidUF_ShouldThrowException_WhenUfNotExists()
    {
        // Arrange
        var validUFs = new string[] { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" };
        var value = "XX";
        var expectedMessage = $"UF inválido. Valores válidos: '{string.Join(", ", validUFs)}'";

        // Act & Assert
        FluentActions.Invoking(() => Validation.DomainValidation.PossibleValidUF(value, "UF"))
            .Should().Throw<EntityValidationException>()
            .WithMessage(expectedMessage);
    }
}