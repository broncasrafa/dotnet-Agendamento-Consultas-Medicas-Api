using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.DomainValidation;

public class PossibleValidPasswordTest
{
    [Theory]
    [InlineData("SecureP@ss1", 5)]
    [InlineData("Valid123!", 5)]
    [InlineData("Usuario@123", 8)]
    public void PossibleValidPassword_ShouldNotThrowException_WhenPasswordIsValid(string value, int minimumLength)
    {
        // Act
        var exception = Record.Exception(() => Validation.DomainValidation.PossibleValidPassword(value, minimumLength));
        // Assert
        exception.Should().BeNull();
    }


    [Theory]
    [InlineData("abcdE1!", 10, "Senha deve ter pelo menos 10 caracteres")]
    [InlineData("abcdefg1@", 5, "Senha deve ter pelo menos 1 letra maiúscula")]
    [InlineData("ABCDEFG1@", 5, "Senha deve ter pelo menos 1 letra minúscula")]
    [InlineData("Abcdefg@#", 5, "Senha deve ter pelo menos 1 número")]
    [InlineData("Abcdefg12", 5, "Senha deve ter pelo menos 1 caractere especial")]
    public void PossibleValidPassword_ShouldThrowException_WhenPasswordIsInvalid(string value, int minimumLength, string expectedMessage)
    {
        // Act
        var exception = Record.Exception(() => Validation.DomainValidation.PossibleValidPassword(value, minimumLength));
        // Assert
        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain($"A senha não atende aos critérios: {expectedMessage}");
    }

    [Theory]
    [InlineData(null, 5, "Senha não pode ser nulo ou vazio.")]
    [InlineData("", 5, "Senha não pode ser nulo ou vazio.")]
    public void PossibleValidPassword_ShouldThrowException_WhenPasswordIsNullOrEmpty(string value, int minimumLength, string expectedMessage)
    {
        // Act
        var exception = Record.Exception(() => Validation.DomainValidation.PossibleValidPassword(value, minimumLength));
        // Assert
        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain(expectedMessage);
    }
}