using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using RSF.AgendamentoConsultas.Core.Domain.Tests.Fixtures;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.Entities;

[Collection(nameof(EspecialidadeTestFixture))]
public class EspecialidadeTest(EspecialidadeTestFixture fixture)
{
    private readonly EspecialidadeTestFixture _fixture = fixture;

    [Fact(DisplayName = "Deve validar objeto com sucesso")]
    public void Especialidade_ShouldBeValid_WhenAddNew_AllFieldsIsValid()
    {
        // Arrange
        var obj = _fixture.GetEspecialidade();

        // Act & Assert
        FluentActions.Invoking(() => obj).Should().NotThrow();

        obj.Should().NotBeNull();
    }

    [Fact(DisplayName = "Deve validar objeto atualizado com sucesso")]
    public void Especialidade_ShouldBeValid_WhenUpdate_AllFieldsIsValid()
    {
        // Arrange
        var obj = _fixture.GetEspecialidade();

        // Act & Assert
        FluentActions.Invoking(() => obj.Update(_fixture.Faker.Commerce.ProductName(), _fixture.Faker.Commerce.ProductName(), 1))
            .Should().NotThrow();

        obj.Should().NotBeNull();
    }

    [Theory(DisplayName = "Deve lançar exceção para campos nulos ou vazios")]
    [InlineData("", 1, "Nome não pode ser nulo ou vazio.")]
    [InlineData(null, 1, "Nome não pode ser nulo ou vazio.")]
    [InlineData("Teste", 0, "EspecialidadeGrupoId deve ser maior que zero.")]
    public void Especialidade_ShouldThrowException_WhenAddNew_FieldsAreInvalids(string nome, int especialidadeGrupoId, string expectedMessage)
    {
        // Act
        var ex = Assert.Throws<EntityValidationException>(() => new Especialidade(nome, _fixture.Faker.Commerce.ProductName(), especialidadeGrupoId));
        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Theory(DisplayName = "Deve lançar exceção na atualização para campos nulos ou vazios")]
    [InlineData("", 1, "Nome não pode ser nulo ou vazio.")]
    [InlineData(null, 1, "Nome não pode ser nulo ou vazio.")]
    [InlineData("Teste", 0, "EspecialidadeGrupoId deve ser maior que zero.")]
    public void Especialidade_ShouldThrowException_WhenUpdate_FieldsAreInvalids(string nome, int especialidadeGrupoId, string expectedMessage)
    {
        // Arrange
        var obj = _fixture.GetEspecialidade();
        // Act
        var ex = Assert.Throws<EntityValidationException>(() => obj.Update(nome, _fixture.Faker.Commerce.ProductName(), especialidadeGrupoId));
        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }
}