using RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.CancelAgendamentoByPaciente;
using FluentAssertions;


namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Agendamento.Command.CancelAgendamentoByPaciente;

public class CancelAgendamentoByPacienteRequestValidatorTest
{
    private readonly CancelAgendamentoByPacienteRequestValidator _validator;
    public CancelAgendamentoByPacienteRequestValidatorTest() => _validator = new CancelAgendamentoByPacienteRequestValidator();


    [Theory(DisplayName = "Deve retornar erro ao validar os identificadores (IDs)")]
    [InlineData(0, "O ID do Agendamento deve ser maior que 0")]
    [InlineData(-1, "O ID do Agendamento deve ser maior que 0")]
    [InlineData(0, "O ID do Paciente deve ser maior que 0")]
    [InlineData(-1, "O ID do Paciente deve ser maior que 0")]
    [InlineData(0, "O ID do Dependente deve ser maior que 0")]
    [InlineData(-1, "O ID do Dependente deve ser maior que 0")]
    public void Validate_ShouldHaveError_When_Ids_IsInvalid(int id, string expectedMessage)
    {
        // Arrange
        var request = new CancelAgendamentoByPacienteRequest(
            AgendamentoId: id,
            PacienteId: id,
            DependenteId: id,
            MotivoCancelamento: "Teste de validacoes");

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }


    [Theory(DisplayName = "Deve retornar erro ao validar o Motivo do Cancelamento")]
    [InlineData(null, "O Motivo do cancelamento é obrigatório, não deve ser nulo ou vazio")]
    [InlineData("", "O Motivo do cancelamento é obrigatório, não deve ser nulo ou vazio")]
    [InlineData("Test", "O Motivo do cancelamento deve ter pelo menos 5 caracteres")]
    public void Validate_ShouldHaveError_When_MotivoCancelamento_IsInvalid(string MotivoCancelamento, string expectedMessage)
    {
        // Arrange
        var request = new CancelAgendamentoByPacienteRequest(
            AgendamentoId: 1,
            PacienteId: 2,
            DependenteId: 1,
            MotivoCancelamento: MotivoCancelamento);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }


    [Fact(DisplayName = "Deve validar os dados de entrada com resultado de sucesso")]
    public void Validate_ShouldBeValid_When_AllInputs_IsValid()
    {
        // Arrange
        var request = new CancelAgendamentoByPacienteRequest(
            AgendamentoId: 1,
            PacienteId: 2,
            DependenteId: 1,
            MotivoCancelamento: "Teste de validacoes");

        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
}