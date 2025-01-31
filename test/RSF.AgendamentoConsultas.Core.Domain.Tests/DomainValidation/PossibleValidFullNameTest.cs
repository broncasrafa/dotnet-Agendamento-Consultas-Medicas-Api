using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.DomainValidation;

public class PossibleValidFullNameTest
{
    [Theory(DisplayName = "Deve validar nome completo corretamente")]
    [InlineData("Usuario Teste")]
    [InlineData("Usuario da Silva Teste")]
    [InlineData("Usuario dos Teste")]
    public void PossibleValidFullName_ShouldNotThrowException_WhenFullNameIsValid(string value)
    {
        // Act & Assert
        FluentActions.Invoking(() => Validation.DomainValidation.PossibleValidFullName(value, "Nome Completo"))
            .Should().NotThrow();
    }

    [Theory(DisplayName = "Deve lançar exceção para valores nulos ou vazios")]
    [InlineData(null, "Nome Completo não pode ser nulo ou vazio.")]
    [InlineData("", "Nome Completo não pode ser nulo ou vazio.")]
    [InlineData(" ", "Nome Completo não pode ser nulo ou vazio.")]
    public void PossibleValidFullName_ShouldThrowException_WhenFullNameIsNullOrEmpty(string value, string expectedMessage)
    {
        // Act & Assert
        FluentActions.Invoking(() => Validation.DomainValidation.PossibleValidFullName(value, "Nome Completo"))
            .Should().Throw<EntityValidationException>()
            .WithMessage(expectedMessage);
    }

    [Theory(DisplayName = "Deve lançar exceção para valores inválidos")]
    [InlineData("Usuario", "Nome Completo inválido.")]
    [InlineData("teste", "Nome Completo inválido.")]
    [InlineData("Usuario ", "Nome Completo inválido.")]
    [InlineData(" Usuario", "Nome Completo inválido.")]
    public void PossibleValidFullName_ShouldThrowException_WhenFullNameIsInvalid(string value, string expectedMessage)
    {
        // Act & Assert
        FluentActions.Invoking(() => Validation.DomainValidation.PossibleValidFullName(value, "Nome Completo"))
            .Should().Throw<EntityValidationException>()
            .WithMessage(expectedMessage);
    }
}