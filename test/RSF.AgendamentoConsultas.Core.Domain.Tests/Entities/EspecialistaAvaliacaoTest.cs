using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;
using Bogus;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.Entities;

public class EspecialistaAvaliacaoTest
{
    private readonly Faker _faker;

    public EspecialistaAvaliacaoTest() => _faker = new Faker("pt_BR");

    [Fact]
    public void EspecialistaAvaliacao_ShouldBeValid_WhenAddNew_AllFieldsIsValid()
    {
        // Arrange
        var obj = new EspecialistaAvaliacao(
            especialistaId: 1, 
            pacienteId: 1, 
            feedback: _faker.Lorem.Text(), 
            score: 1, 
            tagId: 1);

        // Act & Assert
        FluentActions.Invoking(() => obj).Should().NotThrow();

        obj.Should().NotBeNull();
    }


    [Theory]
    [InlineData(0, 1, "Teste", 1, 1, "EspecialistaId deve ser maior que zero.")]
    [InlineData(1, 0, "Teste", 1, 1, "PacienteId deve ser maior que zero.")]
    [InlineData(1, 1, null, 1, 1, "Feedback não pode ser nulo ou vazio.")]
    [InlineData(1, 1, "", 1, 1, "Feedback não pode ser nulo ou vazio.")]
    [InlineData(1, 1, "Teste", 0, 1, "Score inválido. Valores válidos: '1, 2, 3, 4, 5'")]
    [InlineData(1, 1, "Teste", 8, 1, "Score inválido. Valores válidos: '1, 2, 3, 4, 5'")]
    [InlineData(1, 1, "Teste", 1, 0, "TagId deve ser maior que zero.")]
    public void EspecialistaAvaliacao_ShouldThrowException_WhenAddNew_FieldsAreInvalid(int especialistaId, int pacienteId, string feedback, int score, int tagId, string expectedMessage)
    {
        var ex = Assert.Throws<EntityValidationException>(() => 
            new EspecialistaAvaliacao(especialistaId, pacienteId, feedback, score, tagId));

        Assert.Equal(expectedMessage, ex.Message);
    }
}