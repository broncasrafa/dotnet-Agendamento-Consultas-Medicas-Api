using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;
using Bogus;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.Entities;

public class EspecialidadeGrupoTest
{
    private readonly Faker _faker;

    public EspecialidadeGrupoTest() => _faker = new Faker(locale: "pt_BR");


    [Fact(DisplayName = "Deve validar objeto com sucesso")]
    public void EspecialidadeGrupo_ShouldBeValid_WhenAddNew_AllFieldsIsValid()
    {
        // Arrange
        var obj = new EspecialidadeGrupo(nome: _faker.Lorem.Text(), nomePlural: _faker.Lorem.Text());

        // Act & Assert
        FluentActions.Invoking(() => obj).Should().NotThrow();

        obj.Should().NotBeNull();
    }

    [Theory(DisplayName = "Deve lançar exceção para campos nulos ou vazios")]
    [InlineData("", "Teste", "Nome não pode ser nulo ou vazio.")]
    [InlineData(null, "Teste", "Nome não pode ser nulo ou vazio.")]
    [InlineData("Teste", "", "NomePlural não pode ser nulo ou vazio.")]
    [InlineData("Teste", null, "NomePlural não pode ser nulo ou vazio.")]
    public void EspecialidadeGrupo_ShouldThrowException_WhenAddNew_FieldsAreNullOrEmpty(string nome, string nomePlural, string expectedMessage)
    {
        // Act
        var ex = Assert.Throws<EntityValidationException>(() => new EspecialidadeGrupo(nome, nomePlural));
        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }


    [Fact(DisplayName = "Deve validar objeto atualizado com sucesso")]
    public void EspecialidadeGrupo_ShouldBeValid_WhenUpdate_AllFieldsIsValid()
    {
        // Arrange
        var obj = new EspecialidadeGrupo(nome: _faker.Lorem.Text(), nomePlural: _faker.Lorem.Text());

        // Act & Assert
        FluentActions.Invoking(() => obj.Update(nome: _faker.Lorem.Text(), nomePlural: _faker.Lorem.Text()))
            .Should().NotThrow();

        obj.Should().NotBeNull();
    }

    [Theory(DisplayName = "Deve lançar exceção para campos nulos ou vazios ao atualizar")]
    [InlineData("", "Teste", "Nome não pode ser nulo ou vazio.")]
    [InlineData(null, "Teste", "Nome não pode ser nulo ou vazio.")]
    [InlineData("Teste", "", "NomePlural não pode ser nulo ou vazio.")]
    [InlineData("Teste", null, "NomePlural não pode ser nulo ou vazio.")]
    public void EspecialidadeGrupo_ShouldThrowException_WhenUpdate_FieldsAreNullOrEmpty(string nome, string nomePlural, string expectedMessage)
    {
        // Arrange
        var obj = new EspecialidadeGrupo(_faker.Commerce.ProductName(), _faker.Commerce.ProductName());
        // Act
        var ex = Assert.Throws<EntityValidationException>(() => obj.Update(nome, nomePlural));
        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }
}