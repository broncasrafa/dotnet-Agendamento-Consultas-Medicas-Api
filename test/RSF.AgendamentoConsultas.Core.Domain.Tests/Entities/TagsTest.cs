using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;
using Bogus;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.Entities;

public class TagsTest
{
    private readonly Faker _faker;

    public TagsTest() => _faker = new Faker("pt_BR");

    [Fact]
    public void Tags_ShouldBeValid_WhenAddNew_AllFieldsIsValid()
    {
        // Act & Assert
        FluentActions.Invoking(() => new Tags(_faker.Person.Company.Name))
            .Should().NotThrow();
    }

    [Fact]
    public void Tags_ShouldBeValid_WhenUpdate_AllFieldsIsValid()
    {
        // Arrange
        var obj = new Tags(_faker.Person.Company.Name);
        // Act & Assert
        FluentActions.Invoking(() => obj.Update(_faker.Company.CompanyName()))
            .Should().NotThrow();
    }

    [Theory]
    [InlineData("", "Descricao não pode ser nulo ou vazio.")]
    [InlineData(null, "Descricao não pode ser nulo ou vazio.")]
    public void EspecialistaEspecialidade_ShouldThrowException_WhenAddNew_FieldsAreInvalid(string descricao, string expectedMessage)
    {
        var ex = Assert.Throws<EntityValidationException>(() => new Tags(descricao));
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Theory]
    [InlineData("", "Descricao não pode ser nulo ou vazio.")]
    [InlineData(null, "Descricao não pode ser nulo ou vazio.")]
    public void EspecialistaEspecialidade_ShouldThrowException_WhenUpdate_FieldsAreInvalid(string descricao, string expectedMessage)
    {
        // Arrange
        var obj = new Tags(_faker.Person.Company.Name);
        // Act
        var ex = Assert.Throws<EntityValidationException>(() => obj.Update(descricao));
        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }
}