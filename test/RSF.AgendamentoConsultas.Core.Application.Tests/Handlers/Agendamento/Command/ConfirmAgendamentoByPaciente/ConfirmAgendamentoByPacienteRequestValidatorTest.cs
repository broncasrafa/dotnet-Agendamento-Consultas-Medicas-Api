using RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.ConfirmAgendamentoByPaciente;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Agendamento.Command.ConfirmAgendamentoByPaciente;

public class ConfirmAgendamentoByPacienteRequestValidatorTest
{
    private readonly ConfirmAgendamentoByPacienteRequestValidator _validator;

    public ConfirmAgendamentoByPacienteRequestValidatorTest() => _validator = new();

    [Fact(DisplayName = "Deve validar os dados de entrada com resultado de sucesso")]
    public void Validate_ShouldBeValid_When_AllInputs_IsValid()
    {
        // Arrange
        var request = new ConfirmAgendamentoByPacienteRequest(
            AgendamentoId: 1,
            PacienteId: 1);

        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }


    [Theory(DisplayName = "Deve retornar erro ao validar os identificadores (IDs)")]
    [InlineData(0, "O ID do Agendamento deve ser maior que 0")]
    [InlineData(-1, "O ID do Agendamento deve ser maior que 0")]
    [InlineData(0, "O ID do Paciente deve ser maior que 0")]
    [InlineData(-1, "O ID do Paciente deve ser maior que 0")]
    public void Validate_ShouldHaveError_When_Ids_IsInvalid(int id, string expectedMessage)
    {
        // Arrange
        var request = new ConfirmAgendamentoByPacienteRequest(
            AgendamentoId: id,
            PacienteId: id);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }
}