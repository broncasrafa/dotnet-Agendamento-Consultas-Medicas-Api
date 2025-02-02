using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.Entities;

public class PerguntaRespostaReacaoTest
{

    [Fact]
    public void PerguntaRespostaReacao_ShouldBeValid_WhenAddNew_AllFieldsIsValid()
    {
        // Act & Assert
        FluentActions.Invoking(() => new PerguntaRespostaReacao(1, 1, ETipoReacaoResposta.Like))
            .Should().NotThrow();
    }

    [Fact]
    public void PerguntaRespostaReacao_ShouldBeValid_WhenUpdate_AllFieldsIsValid()
    {
        // Arrange
        var obj = new PerguntaRespostaReacao(1, 1, ETipoReacaoResposta.Like);

        // Act & Assert
        FluentActions.Invoking(() => obj.Update(ETipoReacaoResposta.None))
            .Should().NotThrow();
    }

    [Theory]
    [InlineData(0, 1, "RespostaId deve ser maior que zero.")]
    [InlineData(1, 0, "PacienteId deve ser maior que zero.")]
    public void PerguntaRespostaReacao_ShouldThrowException_WhenAddNew_FieldsAreInvalid(int respostaId, int pacienteId, string expectedMessage)
    {
        var ex = Assert.Throws<EntityValidationException>(() => new PerguntaRespostaReacao(respostaId, pacienteId, ETipoReacaoResposta.Like));
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Theory(DisplayName = "Deve retornar o valor correto do enum para a string fornecida")]
    [InlineData("Like", ETipoReacaoResposta.Like)]
    [InlineData("LIKE", ETipoReacaoResposta.Like)]
    [InlineData("like", ETipoReacaoResposta.Like)]
    [InlineData("Dislike", ETipoReacaoResposta.Dislike)]
    [InlineData("dislike", ETipoReacaoResposta.Dislike)]
    [InlineData("DISLIKE", ETipoReacaoResposta.Dislike)]
    [InlineData("LOVE", ETipoReacaoResposta.None)]    
    [InlineData("sad", ETipoReacaoResposta.None)]
    [InlineData("angry", ETipoReacaoResposta.None)]
    [InlineData("unknown", ETipoReacaoResposta.None)]
    [InlineData("", ETipoReacaoResposta.None)]
    [InlineData(null, ETipoReacaoResposta.None)]
    public void PerguntaRespostaReacao_ConverterReacaoParaEnum_ShouldReturnCorrectEnumValue_WhenStringIsProvided(string input, ETipoReacaoResposta expected)
    {
        // Act
        var result = PerguntaRespostaReacao.ConverterReacaoParaEnum(input);

        // Assert
        result.Should().Be(expected);
    }
}