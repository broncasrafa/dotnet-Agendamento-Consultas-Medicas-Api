using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using RSF.AgendamentoConsultas.Core.Domain.Validation;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.DomainValidation;

public class PossiblesValidTypesTest
{
    public static readonly IEnumerable<object[]> InvalidTypesData = [];

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void PossiblesValidTypes_Integer_ShouldNotThrowException_WhenValueIsValid(int value)
    {
        // Arrange
        int[] validScores = TypeValids.VALID_SCORES;

        // Act & Assert
        var exception = Record.Exception(() => Validation.DomainValidation.PossiblesValidTypes(validScores, value, "Score"));

        // Não deve lançar exceção
        exception.Should().BeNull();
    }

    [Theory]
    [InlineData(null)]
    [MemberData(nameof(InvalidTypesData))]
    public void PossiblesValidTypes_Integer_ShouldThrowException_WhenValidTypesIsNullOrEmpty(int[] validTypes)
    {
        // Act
        var exception = Record.Exception(() => Validation.DomainValidation.PossiblesValidTypes(validTypes, 1, "Score"));

        // Assert
        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain("A lista de tipos validos não pode ser nulo ou vazio.");
    }

    [Theory]
    [InlineData(null, "Score não pode ser nulo ou vazio.")]
    [InlineData(20, "Score inválido. Valores válidos: '1, 2, 3, 4, 5'")]
    public void PossiblesValidTypes_Integer_ShouldThrowException_WhenValueIsInvalid(int? value, string expectedMessage)
    {
        // Arrange
        int[] validScores = TypeValids.VALID_SCORES;

        // Act
        var exception = Record.Exception(() => Validation.DomainValidation.PossiblesValidTypes(validScores, value, "Score"));

        // Assert
        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain(expectedMessage);
    }



    [Theory]
    [InlineData("Basic")]
    [InlineData("basic")]
    [InlineData("BASIC")]
    [InlineData("Premium")]
    [InlineData("premium")]
    public void PossiblesValidTypes_String_ShouldNotThrowException_WhenValueIsValid(string value)
    {
        // Arrange
        string[] validCategories = TypeValids.VALID_CATEGORIAS;

        // Act & Assert
        var exception = Record.Exception(() => Validation.DomainValidation.PossiblesValidTypes(validCategories, value, "Categoria"));

        // Não deve lançar exceção
        exception.Should().BeNull();
    }

    [Theory]
    [InlineData(null)]
    [MemberData(nameof(InvalidTypesData))]
    public void PossiblesValidTypes_String_ShouldThrowException_WhenValidTypesIsNullOrEmpty(string[] validTypes)
    {
        // Act
        var exception = Record.Exception(() => Validation.DomainValidation.PossiblesValidTypes(validTypes, "Basic", "Categoria"));

        // Assert
        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain("A lista de tipos validos não pode ser nulo ou vazio.");
    }

    [Theory]
    [InlineData(null, "Categoria não pode ser nulo ou vazio.")]
    [InlineData("", "Categoria não pode ser nulo ou vazio.")]
    [InlineData("Gold", "Categoria inválido. Valores válidos: 'Basic, Premium'")]
    public void PossiblesValidTypes_String_ShouldThrowException_WhenValueIsInvalid(string value, string expectedMessage)
    {
        // Arrange
        string[] validCategories = TypeValids.VALID_CATEGORIAS;

        // Act
        var exception = Record.Exception(() => Validation.DomainValidation.PossiblesValidTypes(validCategories, value, "Categoria"));

        // Assert
        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain(expectedMessage);
    }
}