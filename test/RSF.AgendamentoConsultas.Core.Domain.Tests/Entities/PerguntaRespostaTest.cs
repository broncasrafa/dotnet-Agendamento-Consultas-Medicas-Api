using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;
using Bogus;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.Entities;

public class PerguntaRespostaTest
{
    private readonly Faker _faker;

    public PerguntaRespostaTest() => _faker = new Faker("pt_BR");


    [Fact]
    public void PerguntaResposta_ShouldBeValid_WhenAddNew_AllFieldsIsValid()
    {
        // Arrange
        var obj = new PerguntaResposta(
            perguntaId: 1,
            especialistaId: 1,
            resposta: _faker.Lorem.Text()
        );

        // Act & Assert
        FluentActions.Invoking(() => obj).Should().NotThrow();

        obj.Should().NotBeNull();
    }

    [Theory]
    [InlineData(0, 1, "Teste", "PerguntaId deve ser maior que zero.")]
    [InlineData(1, 0, "Teste", "EspecialistaId deve ser maior que zero.")]
    [InlineData(1, 1, null, "Resposta não pode ser nulo ou vazio.")]
    [InlineData(1, 1, "", "Resposta não pode ser nulo ou vazio.")]
    public void PerguntaResposta_ShouldThrowException_WhenAddNew_FieldsAreInvalid(int perguntaId, int especialistaId, string resposta, string expectedMessage)
    {
        var ex = Assert.Throws<EntityValidationException>(() =>
            new PerguntaResposta(perguntaId, especialistaId, resposta));

        Assert.Equal(expectedMessage, ex.Message);
    }
}