using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.DomainValidation;

public class PossibleValidTimeTest
{
    [Theory]
    [InlineData("14:30")]
    [InlineData("00:00")]
    [InlineData("23:59")]
    public void PossibleValidTime_ShouldNotThrowException_WhenTimeIsValid(string value)
    {
        // Act
        var exception = Record.Exception(() => Validation.DomainValidation.PossibleValidTime(value, "Hora"));
        // Assert
        exception.Should().BeNull();
    }

    [Theory]
    [InlineData(null, "Horário não pode ser nulo ou vazio.")]
    [InlineData("", "Horário não pode ser nulo ou vazio.")]
    [InlineData("25:00", "Horário deve estar no formato válido HH:mm.")]
    [InlineData("8:00", "Horário deve estar no formato válido HH:mm.")]
    [InlineData("24:00", "Horário deve estar no formato válido HH:mm.")]
    [InlineData("23:60", "Horário deve estar no formato válido HH:mm.")]
    public void PossibleValidTime_ShouldNotThrowException_WhenTimeIsInvalid(string value, string expectedMessage)
    {
        var exception = Record.Exception(() => Validation.DomainValidation.PossibleValidTime(value, "Horário"));

        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain(expectedMessage);
    }
}