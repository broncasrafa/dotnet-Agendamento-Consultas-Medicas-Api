using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.DomainValidation;

public class PossibleValidPhoneNumberTest
{
    [Fact]
    public void PossibleValidPhoneNumber_ShouldNotThrowException_WhenPhoneNumberIsValid()
    {
        // Arrange
        string validPhoneWith10Digits = "1234567890"; // Telefone fixo com 10 dígitos
        string validPhoneWith11Digits = "98765432101"; // Telefone celular com 11 dígitos

        // Act
        var validPhone10Exception = Record.Exception(() => Validation.DomainValidation.PossibleValidPhoneNumber(validPhoneWith10Digits, "Telefone"));
        var validPhone11Exception = Record.Exception(() => Validation.DomainValidation.PossibleValidPhoneNumber(validPhoneWith11Digits, "Telefone"));

        // Assert
        validPhone10Exception.Should().BeNull();  // Espera-se que não ocorra exceção para telefone fixo válido
        validPhone11Exception.Should().BeNull();  // Espera-se que não ocorra exceção para telefone celular válido
    }

    [Theory]
    [InlineData("", "Telefone não pode ser nulo ou vazio.")]
    [InlineData("invalid-phone", "Telefone deve conter apenas números.")]
    [InlineData("12345", "Telefone deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    [InlineData("123456789012", "Telefone deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    public void PossibleValidPhoneNumber_ShouldThrowException_WhenPhoneNumberIsInvalid(string phoneValue, string expectedMessage)
    {
        // Act
        var exception = Record.Exception(() => Validation.DomainValidation.PossibleValidPhoneNumber(phoneValue, "Telefone"));

        // Assert
        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain(expectedMessage);
    }

    // Teste para telefone obrigatório
    [Fact]
    public void PossibleValidPhoneNumber_ShouldThrowException_WhenPhoneIsRequiredAndEmpty()
    {
        // Arrange
        string phone = "";

        // Act
        var exception = Record.Exception(() => Validation.DomainValidation.PossibleValidPhoneNumber(phone, "Telefone"));

        // Assert
        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain("Telefone não pode ser nulo ou vazio.");
    }

    // Teste para telefone não obrigatório, mas com valor inválido
    [Fact]
    public void PossibleValidPhoneNumber_ShouldNotThrowException_WhenPhoneIsNotRequiredAndEmpty()
    {
        // Arrange
        string phone = "";

        // Act
        var exception = Record.Exception(() => Validation.DomainValidation.PossibleValidPhoneNumber(phone, "Telefone", isRequired: false));

        // Assert
        exception.Should().BeNull();  // Não deve lançar exceção se não for obrigatório
    }
}