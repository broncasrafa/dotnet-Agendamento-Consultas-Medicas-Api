using Bogus;
using FluentAssertions;

using RSF.AgendamentoConsultas.Core.Application.Features.Avaliacao.Command.CreateAvaliacao;
using RSF.AgendamentoConsultas.Core.Application.Features.Avaliacao.Query.GetAvaliacaoById;


namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Avaliacao.Query.GetAvaliacaoById;

public class SelectAvaliacaoByIdRequestValidatorTest
{
    private readonly SelectAvaliacaoByIdRequestValidator _validator;

    public SelectAvaliacaoByIdRequestValidatorTest() => _validator = new();

    [Fact(DisplayName = "Deve validar os dados de entrada com resultado de sucesso")]
    public void Validate_ShouldBeValid_When_AllInputs_IsValid()
    {
        // Arrange
        var request = new SelectAvaliacaoByIdRequest(Id: 1);

        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory(DisplayName = "Deve retornar erro ao validar os identificadores (IDs)")]
    [InlineData(0, "O ID da avaliação deve ser maior que 0")]
    [InlineData(-1, "O ID da avaliação deve ser maior que 0")]
    public void Validate_ShouldHaveError_When_Ids_IsInvalid(int id, string expectedMessage)
    {
        // Arrange
        var request = new SelectAvaliacaoByIdRequest(Id: id);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }
}