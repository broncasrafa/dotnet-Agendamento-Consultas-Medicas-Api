using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;
using Bogus;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.Entities;

public class PerguntaTest
{
    private readonly Faker _faker;

    public PerguntaTest() => _faker = new Faker("pt_BR");


    [Fact]
    public void Pergunta_ShouldBeValid_WhenAddNew_AllFieldsIsValid()
    {
        // Arrange
        var obj = new Pergunta(
            especialidadeId: 1,
            pacienteId: 1,
            pergunta: _faker.Lorem.Text(),
            termosUsoPolitica: _faker.PickRandomParam(new bool[] { true, false })
        );

        // Act & Assert
        FluentActions.Invoking(() => obj).Should().NotThrow();

        obj.Should().NotBeNull();
    }

    [Theory]
    [InlineData(0, 1, "Teste", true, "EspecialidadeId deve ser maior que zero.")]
    [InlineData(1, 0, "Teste", true, "PacienteId deve ser maior que zero.")]
    [InlineData(1, 1, null, true, "Texto não pode ser nulo ou vazio.")]
    [InlineData(1, 1, "", true, "Texto não pode ser nulo ou vazio.")]
    public void Pergunta_ShouldThrowException_WhenAddNew_FieldsAreInvalid(int especialidadeId, int pacienteId, string pergunta, bool termosUsoPolitica, string expectedMessage)
    {
        var ex = Assert.Throws<EntityValidationException>(() =>
            new Pergunta(especialidadeId, pacienteId, pergunta, termosUsoPolitica));

        Assert.Equal(expectedMessage, ex.Message);
    }
}