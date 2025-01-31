using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.DomainValidation;

public class PossibleValidEmailAddressTest
{

    [Theory]
    [InlineData("teste@teste.com")]
    [InlineData("teste123@teste.com.br")]
    [InlineData("teste_123.usuario@teste123.com")]
    [InlineData("usuario@exemplo.com")]
    [InlineData("email.valido+teste@dominio.net")]
    [InlineData("nome.sobrenome@empresa.org")]
    public void PossibleValidEmailAddress_ShouldNotThrowException_WhenEmailAddressIsValid(string value)
    {
        // Act & Assert
        FluentActions.Invoking(() => Validation.DomainValidation.PossibleValidEmailAddress(value, "Email"))
            .Should().NotThrow();
    }

    [Theory]
    [InlineData(null, true, "Email não pode ser nulo ou vazio.")]
    [InlineData("", true, "Email não pode ser nulo ou vazio.")]
    [InlineData("email_invalido@", true, "Email com valor inválido.")]
    [InlineData("usuario.com.br", true, "Email com valor inválido.")]
    public void PossibleValidEmailAddress_ShouldThrowException_WhenEmailAddressIsInvalid(string email, bool isRequired, string expectedMessage)
    {
        // Act & Assert
        FluentActions.Invoking(() => Validation.DomainValidation.PossibleValidEmailAddress(email, "Email", isRequired))
            .Should().Throw<EntityValidationException>()
            .WithMessage(expectedMessage);
    }

    [Fact]
    public void PossibleValidEmailAddress_ShouldNotThrowException_WhenEmailAddressIsNullOrEmptyAndNotRequired()
    {
        // Act & Assert
        FluentActions.Invoking(() => Validation.DomainValidation.PossibleValidEmailAddress(null, "Email", false))
            .Should().NotThrow();

        FluentActions.Invoking(() => Validation.DomainValidation.PossibleValidEmailAddress("", "Email", false))
            .Should().NotThrow();
    }
}