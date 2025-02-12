using RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Query.GetAgendamentoById;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Agendamento.Query.GetAgendamentoById;

public class SelectAgendamentoByIdRequestValidatorTest
{
    private readonly SelectAgendamentoByIdRequestValidator _validator;

    public SelectAgendamentoByIdRequestValidatorTest()
    {
        _validator = new SelectAgendamentoByIdRequestValidator();
    }

    [Fact(DisplayName = "Deve validar os dados de entrada com resultado de sucesso")]
    public void Validate_ShouldBeValid_When_AllInputs_IsValid()
    {
        // Arrange
        var request = new SelectAgendamentoByIdRequest(1);

        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }


    [Theory(DisplayName = "Deve retornar erro ao validar o Id do agendamento")]
    [InlineData(0, "O ID do Agendamento deve ser maior que 0")]
    [InlineData(-1, "O ID do Agendamento deve ser maior que 0")]
    public void Validate_ShouldHaveError_When_AgendamentoId_IsInvalid(int id, string expectedMessage)
    {
        //Arrange
        var request = new SelectAgendamentoByIdRequest(id);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }
}