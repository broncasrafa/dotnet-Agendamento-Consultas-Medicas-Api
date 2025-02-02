using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.DomainValidation;

public class PossibleValidCpfTest
{
    [Fact]
    public void PossibleValidCpf_ShouldNotThrowException_WhenValueIsValid()
    {
        // Act & Assert
        FluentActions.Invoking(() => Validation.DomainValidation.PossibleValidCpf("29714777976"))
            .Should().NotThrow();
    }

    [Theory(DisplayName = "Deve lançar exceção para valores inválidos")]
    [InlineData(null, "CPF não pode ser nulo ou vazio.")]
    [InlineData("", "CPF não pode ser nulo ou vazio.")]
    [InlineData("4431305", "CPF deve conter exatamente 11 digitos.")]
    [InlineData("44313054785412", "CPF deve conter exatamente 11 digitos.")]
    [InlineData("4431305ac4",  "CPF deve conter apenas números.")]
    [InlineData("44313050124", "CPF inválido.")]
    [InlineData("11111111111", "CPF inválido.")]
    public void PossibleValidCpf_ShouldThrowException_WhenValueIsInvalid(string invalidNumber, string expectedMessage)
    {
        // Act & Assert
        var exception = Record.Exception(() => Validation.DomainValidation.PossibleValidCpf(invalidNumber));

        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain(expectedMessage);
    }
}