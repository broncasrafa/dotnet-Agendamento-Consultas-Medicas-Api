using FluentAssertions;
using RSF.AgendamentoConsultas.Core.Domain.Exceptions;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.DomainValidation;

public class NotNullOrEmptyTest
{
    [Fact]
    public void NotNullOrEmpty_ShouldNotThrowException_WhenValueIsNotNullOrEmpty()
    {
        // Act & Assert
        var exception = Record.Exception(() => Validation.DomainValidation.NotNullOrEmpty("Teste", "Nome"));
        // Não deve lançar exceção
        exception.Should().BeNull();
    }

    [Theory]
    [InlineData(null, "Campo não pode ser nulo ou vazio.")]
    [InlineData("", "Campo não pode ser nulo ou vazio.")]
    public void NotNullOrEmpty_ShouldThrowException_WhenValueIsNullOrEmpty(string value, string expectedMessage)
    {
        // Act & Assert
        var exception = Record.Exception(() => Validation.DomainValidation.NotNullOrEmpty(value, "Campo"));

        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain(expectedMessage);
    }
}