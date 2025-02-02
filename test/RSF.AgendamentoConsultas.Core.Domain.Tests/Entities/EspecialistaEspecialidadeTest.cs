using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;
using Bogus;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.Entities;

public class EspecialistaEspecialidadeTest
{
    private readonly Faker _faker;
    private readonly EspecialistaEspecialidade _especialistaEspecialidadeMock;

    public EspecialistaEspecialidadeTest()
    {
        _faker = new Faker("pt_BR");
        _especialistaEspecialidadeMock = new EspecialistaEspecialidade(1, 1, "Principal");
    }


    [Fact]
    public void EspecialistaEspecialidade_ShouldBeValid_WhenAddNew_AllFieldsIsValid()
    {
        // Arrange
        var obj = _especialistaEspecialidadeMock;

        // Act & Assert
        FluentActions.Invoking(() => obj).Should().NotThrow();

        obj.Should().NotBeNull();
    }

    [Theory]
    [InlineData(0, "Principal", "EspecialidadeId deve ser maior que zero.")]
    [InlineData(1, "", "TipoEspecialidade não pode ser nulo ou vazio.")]
    [InlineData(1, null, "TipoEspecialidade não pode ser nulo ou vazio.")]
    [InlineData(1, "Teste", "TipoEspecialidade inválido. Valores válidos: 'Principal, SubEspecialidade'")]
    public void EspecialistaEspecialidade_ShouldNotBeValid_WhenAddNew_FieldsAreInvalid(int especialidadeId, string tipoEspecialidade, string expectedMessage)
    {
        var ex = Assert.Throws<EntityValidationException>(() => new EspecialistaEspecialidade(especialidadeId, tipoEspecialidade));
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Theory]
    [InlineData(0, 1, "Principal", "EspecialistaId deve ser maior que zero.")]
    [InlineData(1, 0, "Principal", "EspecialidadeId deve ser maior que zero.")]
    [InlineData(1, 1, "", "TipoEspecialidade não pode ser nulo ou vazio.")]
    [InlineData(1, 1, null, "TipoEspecialidade não pode ser nulo ou vazio.")]
    [InlineData(1, 1, "Teste", "TipoEspecialidade inválido. Valores válidos: 'Principal, SubEspecialidade'")]
    public void EspecialistaEspecialidade_ShouldNotBeValid_WhenAddNew_FieldsAreInvalid2(int especialistaId, int especialidadeId, string tipoEspecialidade, string expectedMessage)
    {
        var ex = Assert.Throws<EntityValidationException>(() => new EspecialistaEspecialidade(especialistaId, especialidadeId, tipoEspecialidade));
        Assert.Equal(expectedMessage, ex.Message);
    }



    [Fact]
    public void EspecialistaEspecialidade_ShouldBeValid_WhenUpdate_AllFieldsIsValid()
    {
        // Arrange
        var obj = _especialistaEspecialidadeMock;

        // Act & Assert
        FluentActions.Invoking(() => obj.Update(2, "Principal")).Should().NotThrow();

        obj.Should().NotBeNull();
    }

    [Theory]
    [InlineData(0, "Principal", "EspecialidadeId deve ser maior que zero.")]
    [InlineData(1, "", "TipoEspecialidade não pode ser nulo ou vazio.")]
    [InlineData(1, null, "TipoEspecialidade não pode ser nulo ou vazio.")]
    [InlineData(1, "Teste", "TipoEspecialidade inválido. Valores válidos: 'Principal, SubEspecialidade'")]
    public void EspecialistaEspecialidade_ShouldNotBeValid_WhenUpdate_FieldsAreInvalid(int especialidadeId, string tipoEspecialidade, string expectedMessage)
    {
        var obj = _especialistaEspecialidadeMock;
        var ex = Assert.Throws<EntityValidationException>(() => obj.Update(especialidadeId, tipoEspecialidade));
        Assert.Equal(expectedMessage, ex.Message);
    }
}