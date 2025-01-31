using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.DomainValidation;

public class PossibleValidDateTest
{
    [Fact]
    public void PossibleValidDate_ShouldNotThrowException_WhenValueIsValid()
    {
        // Arrange
        var futureDate = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd"); // Data futura dinâmica
        var pastDate = DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd"); // Data passada dinâmica

        // Act
        var futureException = Record.Exception(() => Validation.DomainValidation.PossibleValidDate(futureDate, true, "Data de Consulta"));
        var pastException = Record.Exception(() => Validation.DomainValidation.PossibleValidDate(pastDate, false, "Data de Consulta"));

        // Assert
        futureException.Should().BeNull();  // Espera-se que não ocorra exceção para data futura
        pastException.Should().BeNull();    // Espera-se que não ocorra exceção para data passada
    }

    [Theory]
    [InlineData("", "Data de Consulta não pode ser nulo ou vazio.")]
    [InlineData("invalid-date", "Data de Consulta está em um formato inválido. O formato correto é yyyy-MM-dd.")]
    public void PossibleValidDate_ShouldThrowException_WhenDateIsInvalid(string dateValue, string expectedMessage)
    {
        // Act
        var exception = Record.Exception(() => Validation.DomainValidation.PossibleValidDate(dateValue, true, "Data de Consulta"));

        // Assert
        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain(expectedMessage);
    }

    [Fact]
    public void PossibleValidDate_ShouldThrowException_WhenDateIsNotFuture()
    {
        // Arrange
        var pastDate = DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd");

        // Act
        var pastException = Record.Exception(() => Validation.DomainValidation.PossibleValidDate(pastDate, true, "Data de Consulta"));

        // Assert
        pastException.Should().BeOfType<EntityValidationException>(); // Espera-se exceção para data passada
        pastException.Message.Should().Contain("Data de Consulta deve ser uma data futura e maior que hoje.");
    }

    [Fact]
    public void PossibleValidDate_ShouldThrowException_WhenDateIsNotPast()
    {
        // Arrange
        var futureDate = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd");

        // Act
        var futureException = Record.Exception(() => Validation.DomainValidation.PossibleValidDate(futureDate, false, "Data de Consulta"));

        // Assert
        futureException.Should().BeOfType<EntityValidationException>(); // Espera-se exceção para data futura
        futureException.Message.Should().Contain("Data de Consulta deve ser uma data passada e menor que hoje.");
    }
}