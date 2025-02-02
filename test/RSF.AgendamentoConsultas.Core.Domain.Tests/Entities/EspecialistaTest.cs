using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using RSF.AgendamentoConsultas.Core.Domain.Tests.Fixtures;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.Entities;

[Collection(nameof(EspecialistaTestFixture))]
public class EspecialistaTest(EspecialistaTestFixture fixture)
{
    private readonly EspecialistaTestFixture _fixture = fixture;


    [Fact(DisplayName = "Deve validar objeto com sucesso")]
    public void Especialista_ShouldBeValid_WhenAddNew_AllFieldsIsValid()
    {
        // Arrange
        var obj = _fixture.GetEntity();

        // Act & Assert
        FluentActions.Invoking(() => obj).Should().NotThrow();

        obj.Should().NotBeNull();
    }

    [Fact(DisplayName = "Deve validar objeto atualizado com sucesso")]
    public void Especialista_ShouldBeValid_WhenUpdate_AllFieldsIsValid()
    {
        // Arrange
        var obj = _fixture.GetEntity();

        // Act & Assert
        FluentActions.Invoking(() => obj.Update(_fixture.Faker.Person.FullName))
            .Should().NotThrow();

        obj.Should().NotBeNull();
    }

    
    [Theory]
    [InlineData("", "nome", "licenca", "email@teste.com", "Masculino", "Basic", "UserId não pode ser nulo ou vazio.")]
    [InlineData(null, "nome", "licenca", "email@teste.com", "Masculino", "Basic", "UserId não pode ser nulo ou vazio.")]
    [InlineData("userId", "", "licenca", "email@teste.com", "Masculino", "Basic", "Nome não pode ser nulo ou vazio.")]
    [InlineData("userId", null, "licenca", "email@teste.com", "Masculino", "Basic", "Nome não pode ser nulo ou vazio.")]
    [InlineData("userId", "nome", "", "email@teste.com", "Masculino", "Basic", "Licenca não pode ser nulo ou vazio.")]
    [InlineData("userId", "nome", null, "email@teste.com", "Masculino", "Basic", "Licenca não pode ser nulo ou vazio.")]
    [InlineData("userId", "nome", "licenca", "", "Masculino", "Basic", "Email não pode ser nulo ou vazio.")]
    [InlineData("userId", "nome", "licenca", null, "Masculino", "Basic", "Email não pode ser nulo ou vazio.")]
    [InlineData("userId", "nome", "licenca", "teste@", "Masculino", "Basic", "Email com valor inválido.")]
    [InlineData("userId", "nome", "licenca", "email@teste.com", "genero", "Basic", "Genero inválido. Valores válidos: 'Masculino, Feminino, Não informado'")]
    [InlineData("userId", "nome", "licenca", "email@teste.com", "Masculino", "VIP", "Tipo inválido. Valores válidos: 'Basic, Premium'")]
    public void Especialista_ShouldThrowException_WhenAddNew_FieldsAreInvalids(string userId, string nome, string licenca, string email, string genero, string tipo, string expectedMessage)
    {
        // Arrange & Act
        var ex = Assert.Throws<EntityValidationException>(() => new Especialista(userId, nome, licenca, email, genero, tipo));
        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Theory]
    [InlineData("", "Basic", "Nome não pode ser nulo ou vazio.")]
    [InlineData(null, "Basic", "Nome não pode ser nulo ou vazio.")]
    [InlineData("nome", "VIP", "Tipo inválido. Valores válidos: 'Basic, Premium'")]
    public void Especialista_ShouldThrowException_WhenUpdate_FieldsAreInvalids(string nome, string tipo, string expectedMessage)
    {
        // Arrange
        var obj = _fixture.GetEntity();
        // Act
        var ex = Assert.Throws<EntityValidationException>(() => obj.Update(nome, tipo));
        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }
}