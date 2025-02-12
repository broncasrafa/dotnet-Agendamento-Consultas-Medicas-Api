using RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.CreateAgendamento;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Agendamento.Command.CreateAgendamento;

public class CreateAgendamentoRequestValidatorTest
{
    private readonly CreateAgendamentoRequestValidator _validator;

    public CreateAgendamentoRequestValidatorTest()
    {
        _validator = new CreateAgendamentoRequestValidator();
    }

    [Fact(DisplayName = "Deve validar os dados de entrada com resultado de sucesso")]
    public void Validate_ShouldBeValid_When_AllInputs_IsValid()
    {
        // Arrange
        var request = new CreateAgendamentoRequest
        (
            EspecialistaId: 1,
            EspecialidadeId: 1,
            LocalAtendimentoId: 1,
            TipoConsultaId: 1,
            TipoAgendamentoId: 1,
            DataConsulta: DateTime.Now.AddDays(5),
            HorarioConsulta: "10:45",
            MotivoConsulta: "Teste",
            ValorConsulta: null,
            TelefoneContato: "11945127845",
            PrimeiraVez: true,
            PacienteId: 1,
            DependenteId: null,
            PlanoMedicoId: 1
        );

        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }


    [Theory(DisplayName = "Deve retornar erro ao validar os identificadores (IDs)")]
    [InlineData(0, "O ID do Especialista deve ser maior que 0")]
    [InlineData(0, "O ID da Especialidade deve ser maior que 0")]
    [InlineData(0, "O ID do Local de atendimento deve ser maior que 0")]
    [InlineData(0, "O ID do Tipo de Consulta deve ser maior que 0")]
    [InlineData(0, "O ID do Tipo de Agendamento deve ser maior que 0")]
    [InlineData(0, "O ID do Paciente deve ser maior que 0")]
    [InlineData(0, "O ID do Plano Medico do Paciente/Dependente deve ser maior que 0")]
    [InlineData(0, "O ID do Dependente deve ser maior que 0")]
    public void Validate_ShouldHaveError_When_Ids_IsInvalid(int id, string expectedMessage)
    {
        // Arrange
        var request = new CreateAgendamentoRequest(
            EspecialistaId: id,
            EspecialidadeId: id,
            LocalAtendimentoId: id,
            TipoConsultaId: id,
            TipoAgendamentoId: id,
            DataConsulta: DateTime.Now.AddDays(5),
            HorarioConsulta: "10:45",
            MotivoConsulta: "Teste",
            ValorConsulta: null,
            TelefoneContato: "11945127845",
            PrimeiraVez: true,
            PacienteId: id,
            DependenteId: id,
            PlanoMedicoId: id);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }

    [Theory(DisplayName = "Deve retornar erro ao validar a Data da Consulta")]
    [InlineData("0001-01-01", "A data da consulta é obrigatória, não pode ser nula ou vazia")]
    [InlineData("2022-01-01", "A data da consulta deve ser maior que a data atual")]
    public void Validate_ShouldHaveError_When_DataConsulta_IsInvalid(string dataConsulta, string expectedMessage)
    {
        // Arrange
        var request = new CreateAgendamentoRequest(
            EspecialistaId: 1,
            EspecialidadeId: 1,
            LocalAtendimentoId: 1,
            TipoConsultaId: 1,
            TipoAgendamentoId: 1,
            DataConsulta: DateTime.Parse(dataConsulta),
            HorarioConsulta: "10:45",
            MotivoConsulta: "Teste",
            ValorConsulta: null,
            TelefoneContato: "11945127845",
            PrimeiraVez: true,
            PacienteId: 1,
            DependenteId: null,
            PlanoMedicoId: 1);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }

    [Theory(DisplayName = "Deve retornar erro ao validar o Horario da Consulta")]
    [InlineData(null, "O horário da consulta é obrigatório, não pode ser nulo ou vazio.")]
    [InlineData("", "O horário da consulta é obrigatório, não pode ser nulo ou vazio.")]
    [InlineData("30:59", "O horário da consulta deve estar no formato 'HH:mm' e ser um horário válido.")]
    [InlineData("ab:cd", "O horário da consulta deve estar no formato 'HH:mm' e ser um horário válido.")]
    [InlineData("a1:c3", "O horário da consulta deve estar no formato 'HH:mm' e ser um horário válido.")]
    [InlineData("123:59", "O horário da consulta deve estar no formato 'HH:mm' e ser um horário válido.")]
    public void Validate_ShouldHaveError_When_HorarioConsulta_IsInvalid(string horarioConsulta, string expectedMessage)
    {
        // Arrange
        var request = new CreateAgendamentoRequest(
            EspecialistaId: 1,
            EspecialidadeId: 1,
            LocalAtendimentoId: 1,
            TipoConsultaId: 1,
            TipoAgendamentoId: 1,
            DataConsulta: DateTime.Now.AddDays(5),
            HorarioConsulta: horarioConsulta,
            MotivoConsulta: "Teste",
            ValorConsulta: null,
            TelefoneContato: "11945127845",
            PrimeiraVez: true,
            PacienteId: 1,
            DependenteId: null,
            PlanoMedicoId: 1);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }

    [Theory(DisplayName = "Deve retornar erro ao validar o Motivo da Consulta")]
    [InlineData(null, "O Motivo da consulta é obrigatório, não deve ser nulo ou vazio")]
    [InlineData("", "O Motivo da consulta é obrigatório, não deve ser nulo ou vazio")]
    [InlineData("Test", "O Motivo da consulta deve ter pelo menos 5 caracteres")]
    public void Validate_ShouldHaveError_When_MotivoConsulta_IsInvalid(string MotivoConsulta, string expectedMessage)
    {
        // Arrange
        var request = new CreateAgendamentoRequest(
            EspecialistaId: 1,
            EspecialidadeId: 1,
            LocalAtendimentoId: 1,
            TipoConsultaId: 1,
            TipoAgendamentoId: 1,
            DataConsulta: DateTime.Now.AddDays(5),
            HorarioConsulta: "15:30",
            MotivoConsulta: MotivoConsulta,
            ValorConsulta: null,
            TelefoneContato: "11945127845",
            PrimeiraVez: true,
            PacienteId: 1,
            DependenteId: null,
            PlanoMedicoId: 1);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }

    [Theory(DisplayName = "Deve retornar erro ao validar o Valor da Consulta")]
    [InlineData(0, "O valor da consulta deve ser maior que zero.")]
    [InlineData(-50.89, "O valor da consulta deve ser maior que zero.")]
    [InlineData(10.987, "O valor da consulta deve ter no máximo duas casas decimais.")]
    public void Validate_ShouldHaveError_When_ValorConsulta_IsInvalid(decimal ValorConsulta, string expectedMessage)
    {
        // Arrange
        var request = new CreateAgendamentoRequest(
            EspecialistaId: 1,
            EspecialidadeId: 1,
            LocalAtendimentoId: 1,
            TipoConsultaId: 1,
            TipoAgendamentoId: 1,
            DataConsulta: DateTime.Now.AddDays(5),
            HorarioConsulta: "15:30",
            MotivoConsulta: "Teste",
            ValorConsulta: ValorConsulta,
            TelefoneContato: "11945127845",
            PrimeiraVez: true,
            PacienteId: 1,
            DependenteId: null,
            PlanoMedicoId: 1);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }

    [Theory(DisplayName = "Deve retornar erro ao validar o Telefone de contato")]
    [InlineData(null, "O telefone não pode ser nulo ou vazio.")]
    [InlineData("", "O telefone não pode ser nulo ou vazio.")]
    [InlineData("invalid-phone", "O telefone deve conter apenas números.")]
    [InlineData("12345", "O telefone deve ter 10 dígitos para fixo ou 11 dígitos para celular.")]
    [InlineData("123456789012", "O telefone deve ter 10 dígitos para fixo ou 11 dígitos para celular.")]
    [InlineData("11111111111", "O telefone não deve ter somente números consecutivos iguais")]
    public void Validate_ShouldHaveError_When_TelefoneContato_IsInvalid(string TelefoneContato, string expectedMessage)
    {
        //Arrange
        var request = new CreateAgendamentoRequest(
            EspecialistaId: 1,
            EspecialidadeId: 1,
            LocalAtendimentoId: 1,
            TipoConsultaId: 1,
            TipoAgendamentoId: 1,
            DataConsulta: DateTime.Now.AddDays(5),
            HorarioConsulta: "10:45",
            MotivoConsulta: "Teste",
            ValorConsulta: null,
            TelefoneContato: TelefoneContato,
            PrimeiraVez: true,
            PacienteId: 1,
            DependenteId: null,
            PlanoMedicoId: 1);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }
}
