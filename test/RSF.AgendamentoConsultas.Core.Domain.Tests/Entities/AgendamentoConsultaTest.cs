using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using RSF.AgendamentoConsultas.Core.Domain.Tests.Base;
using RSF.AgendamentoConsultas.Core.Domain.Tests.Fixtures;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using FluentAssertions;
using Bogus;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.Entities;

public class AgendamentoConsultaTest
{
    private readonly Faker _faker;

    public AgendamentoConsultaTest()
    {
        _faker = new Faker(locale: "pt_BR");
    }

    [Fact(DisplayName = "Deve validar objeto corretamente")]
    public void AgendamentoConsulta_ShouldBeValid_WhenAllFieldsIsValid()
    {
        // Arrange
        var especialistaId = 1;
        var especialidadeId = 2;
        var localAtendimentoId = 3;
        var tipoConsultaId = (int)ETipoConsulta.Presencial;
        var tipoAgendamentoId = (int)ETipoAgendamento.Convenio;
        var dataConsulta = DateTime.Now.AddDays(1); // Data futura
        var horarioConsulta = "14:30"; // Horário válido
        var motivoConsulta = _faker.Lorem.Text();
        var valorConsulta = 150.00m;
        var telefoneContato = "11987654321"; // Número de telefone válido
        var primeiraVez = true;
        var pacienteId = 5;
        var dependenteId = (int?)null; // Não há dependente
        var planoMedicoId = 10;

        // Act
        var agendamentoConsulta = new AgendamentoConsulta(
            especialistaId,
            especialidadeId,
            localAtendimentoId,
            tipoConsultaId,
            tipoAgendamentoId,
            dataConsulta,
            horarioConsulta,
            motivoConsulta,
            valorConsulta,
            telefoneContato,
            primeiraVez,
            pacienteId,
            dependenteId,
            planoMedicoId
        );

        // Assert
        agendamentoConsulta.EspecialistaId.Should().Be(especialistaId);
        agendamentoConsulta.EspecialidadeId.Should().Be(especialidadeId);
        agendamentoConsulta.LocalAtendimentoId.Should().Be(localAtendimentoId);
        agendamentoConsulta.TipoConsultaId.Should().Be(tipoConsultaId);
        agendamentoConsulta.TipoAgendamentoId.Should().Be(tipoAgendamentoId);
        agendamentoConsulta.DataConsulta.Should().Be(dataConsulta);
        agendamentoConsulta.HorarioConsulta.Should().Be(horarioConsulta);
        agendamentoConsulta.MotivoConsulta.Should().Be(motivoConsulta);
        agendamentoConsulta.ValorConsulta.Should().Be(null);
        agendamentoConsulta.TelefoneContato.Should().Be(telefoneContato);
        agendamentoConsulta.PrimeiraVez.Should().Be(primeiraVez);
        agendamentoConsulta.PacienteId.Should().Be(pacienteId);
        agendamentoConsulta.DependenteId.Should().Be(dependenteId);
        agendamentoConsulta.PlanoMedicoId.Should().Be(planoMedicoId);
    }



    [Fact(DisplayName = "Deve lançar exceção para EspecialistaId inválido")]
    public void AgendamentoConsulta_ShouldThrowException_WhenEspecialistaIdIsInvalid()
    {
        var ex = Assert.Throws<EntityValidationException>(() =>
            new AgendamentoConsulta(
                especialistaId: 0,
                especialidadeId: 1,
                localAtendimentoId: 1,
                tipoConsultaId: (int)ETipoConsulta.Presencial,
                tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
                dataConsulta: DateTime.Now.AddDays(5),
                horarioConsulta: "12:20",
                motivoConsulta: _faker.Lorem.Text(),
                valorConsulta: null,
                telefoneContato: "1199999565",
                primeiraVez: false,
                pacienteId: 1,
                dependenteId: null,
                planoMedicoId: 1)
        );
        Assert.Equal("EspecialistaId deve ser maior que zero.", ex.Message);
    }

    [Fact(DisplayName = "Deve lançar exceção para EspecialidadeId inválido")]
    public void AgendamentoConsulta_ShouldThrowException_WhenEspecialidadeIdIsInvalid()
    {
        var ex = Assert.Throws<EntityValidationException>(() =>
            new AgendamentoConsulta(
                especialistaId: 1,
                especialidadeId: 0,
                localAtendimentoId: 1,
                tipoConsultaId: (int)ETipoConsulta.Presencial,
                tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
                dataConsulta: DateTime.Now.AddDays(5),
                horarioConsulta: "12:20",
                motivoConsulta: _faker.Lorem.Text(),
                valorConsulta: null,
                telefoneContato: "1199999565",
                primeiraVez: false,
                pacienteId: 1,
                dependenteId: null,
                planoMedicoId: 1)
        );
        Assert.Equal("EspecialidadeId deve ser maior que zero.", ex.Message);
    }

    [Fact(DisplayName = "Deve lançar exceção para LocalAtendimentoId inválido")]
    public void AgendamentoConsulta_ShouldThrowException_WhenLocalAtendimentoIdIsInvalid()
    {
        var ex = Assert.Throws<EntityValidationException>(() =>
            new AgendamentoConsulta(
                especialistaId: 1,
                especialidadeId: 1,
                localAtendimentoId: 0,
                tipoConsultaId: (int)ETipoConsulta.Presencial,
                tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
                dataConsulta: DateTime.Now.AddDays(5),
                horarioConsulta: "12:20",
                motivoConsulta: _faker.Lorem.Text(),
                valorConsulta: null,
                telefoneContato: "1199999565",
                primeiraVez: false,
                pacienteId: 1,
                dependenteId: null,
                planoMedicoId: 1)
        );
        Assert.Equal("LocalAtendimentoId deve ser maior que zero.", ex.Message);
    }

    [Fact(DisplayName = "Deve lançar exceção para PacienteId inválido")]
    public void AgendamentoConsulta_ShouldThrowException_WhenPacienteIdIsInvalid()
    {
        var ex = Assert.Throws<EntityValidationException>(() =>
            new AgendamentoConsulta(
                especialistaId: 1,
                especialidadeId: 1,
                localAtendimentoId: 1,
                tipoConsultaId: (int)ETipoConsulta.Presencial,
                tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
                dataConsulta: DateTime.Now.AddDays(5),
                horarioConsulta: "12:20",
                motivoConsulta: _faker.Lorem.Text(),
                valorConsulta: null,
                telefoneContato: "1199999565",
                primeiraVez: false,
                pacienteId: 0,
                dependenteId: null,
                planoMedicoId: 1)
        );
        Assert.Equal("PacienteId deve ser maior que zero.", ex.Message);
    }


    [Fact(DisplayName = "Deve lançar exceção para PlanoMedicoId inválido")]
    public void AgendamentoConsulta_ShouldThrowException_WhenPlanoMedicoIdIsInvalid()
    {
        var ex = Assert.Throws<EntityValidationException>(() =>
            new AgendamentoConsulta(
                especialistaId: 1,
                especialidadeId: 1,
                localAtendimentoId: 1,
                tipoConsultaId: (int)ETipoConsulta.Presencial,
                tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
                dataConsulta: DateTime.Now.AddDays(5),
                horarioConsulta: "12:20",
                motivoConsulta: _faker.Lorem.Text(),
                valorConsulta: null,
                telefoneContato: "1199999565",
                primeiraVez: false,
                pacienteId: 1,
                dependenteId: null,
                planoMedicoId: 0)
        );
        Assert.Equal("PlanoMedicoId deve ser maior que zero.", ex.Message);
    }


    [Fact(DisplayName = "Deve lançar exceção para DependenteId inválido")]
    public void AgendamentoConsulta_ShouldThrowException_WhenDependenteIdIsInvalid()
    {
        var ex = Assert.Throws<EntityValidationException>(() =>
            new AgendamentoConsulta(
                especialistaId: 1,
                especialidadeId: 1,
                localAtendimentoId: 1,
                tipoConsultaId: (int)ETipoConsulta.Presencial,
                tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
                dataConsulta: DateTime.Now.AddDays(5),
                horarioConsulta: "12:20",
                motivoConsulta: _faker.Lorem.Text(),
                valorConsulta: null,
                telefoneContato: "1199999565",
                primeiraVez: false,
                pacienteId: 1,
                dependenteId: 0,
                planoMedicoId: 5)
        );
        Assert.Equal("DependenteId deve ser maior que zero.", ex.Message);
    }


    [Fact(DisplayName = "Deve lançar exceção para DependentePlanoMedicoId inválido")]
    public void AgendamentoConsulta_ShouldThrowException_WhenDependentePlanoMedicoIdIsInvalid()
    {
        var ex = Assert.Throws<EntityValidationException>(() =>
            new AgendamentoConsulta(
                especialistaId: 1,
                especialidadeId: 1,
                localAtendimentoId: 1,
                tipoConsultaId: (int)ETipoConsulta.Presencial,
                tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
                dataConsulta: DateTime.Now.AddDays(5),
                horarioConsulta: "12:20",
                motivoConsulta: _faker.Lorem.Text(),
                valorConsulta: null,
                telefoneContato: "1199999565",
                primeiraVez: false,
                pacienteId: 1,
                dependenteId: 2,
                planoMedicoId: 0)
        );
        Assert.Equal("PlanoMedicoId deve ser maior que zero.", ex.Message);
    }


    [Theory(DisplayName = "Deve lançar exceção para ValorConsulta inválido")]
    [InlineData(0, "ValorConsulta deve ser maior que zero.")]
    [InlineData(-10.50, "ValorConsulta deve ser maior que zero.")]
    public void AgendamentoConsulta_ShouldThrowException_WhenValorConsultaIsInvalid(decimal valorConsulta, string expectedMessage)
    {
        var ex = Assert.Throws<EntityValidationException>(() =>
            new AgendamentoConsulta(
                especialistaId: 1,
                especialidadeId: 1,
                localAtendimentoId: 1,
                tipoConsultaId: (int)ETipoConsulta.Presencial,
                tipoAgendamentoId: (int)ETipoAgendamento.Particular,
                dataConsulta: DateTime.Now.AddDays(5),
                horarioConsulta: "15:30",
                motivoConsulta: _faker.Lorem.Text(),
                valorConsulta: valorConsulta,
                telefoneContato: "1199999565",
                primeiraVez: false,
                pacienteId: 1,
                dependenteId: null,
                planoMedicoId: 1)
        );
        Assert.Equal(expectedMessage, ex.Message);
    }


    [Theory(DisplayName = "Deve lançar exceção para TipoConsultaId inválido")]
    [InlineData(0, "TipoConsultaId deve ser maior que zero.")]
    [InlineData(20, "TipoConsultaId inválido. Valores válidos: '1, 2'")]
    public void AgendamentoConsulta_ShouldThrowException_WhenTipoConsultaIdIsInvalid(int tipoConsultaId, string expectedMessage)
    {
        var ex = Assert.Throws<EntityValidationException>(() =>
            new AgendamentoConsulta(
                especialistaId: 1,
                especialidadeId: 1,
                localAtendimentoId: 1,
                tipoConsultaId: tipoConsultaId,
                tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
                dataConsulta: DateTime.Now.AddDays(5),
                horarioConsulta: "15:30",
                motivoConsulta: _faker.Lorem.Text(),
                valorConsulta: 158.90m,
                telefoneContato: "11965874510",
                primeiraVez: false,
                pacienteId: 1,
                dependenteId: null,
                planoMedicoId: 1)
        );
        Assert.Equal(expectedMessage, ex.Message);
    }


    [Theory(DisplayName = "Deve lançar exceção para TipoAgendamentoId inválido")]
    [InlineData(0, "TipoAgendamentoId deve ser maior que zero.")]
    [InlineData(20, "TipoAgendamentoId inválido. Valores válidos: '1, 2'")]
    public void AgendamentoConsulta_ShouldThrowException_WhenTipoAgendamentoIdIsInvalid(int tipoAgendamentoId, string expectedMessage)
    {
        var ex = Assert.Throws<EntityValidationException>(() =>
            new AgendamentoConsulta(
                especialistaId: 1,
                especialidadeId: 1,
                localAtendimentoId: 1,
                tipoConsultaId: (int)ETipoConsulta.Presencial,
                tipoAgendamentoId: tipoAgendamentoId,
                dataConsulta: DateTime.Now.AddDays(5),
                horarioConsulta: "15:30",
                motivoConsulta: _faker.Lorem.Text(),
                valorConsulta: 158.90m,
                telefoneContato: "11965874510",
                primeiraVez: false,
                pacienteId: 1,
                dependenteId: null,
                planoMedicoId: 1)
        );
        Assert.Equal(expectedMessage, ex.Message);
    }




    [Theory(DisplayName = "Deve lançar exceção para HorarioConsulta inválido")]
    [InlineData("", "HorarioConsulta não pode ser nulo ou vazio.")]
    [InlineData("24:00", "HorarioConsulta deve estar no formato válido HH:mm.")]
    public void AgendamentoConsulta_ShouldThrowException_WhenHorarioConsultaIsInvalid(string horarioConsulta, string expectedMessage)
    {
        var ex = Assert.Throws<EntityValidationException>(() =>
            new AgendamentoConsulta(
                especialistaId: 1,
                especialidadeId: 1,
                localAtendimentoId: 1,
                tipoConsultaId: (int)ETipoConsulta.Presencial,
                tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
                dataConsulta: DateTime.Now.AddDays(5),
                horarioConsulta: horarioConsulta,
                motivoConsulta: _faker.Lorem.Text(),
                valorConsulta: null,
                telefoneContato: "1199999565",
                primeiraVez: false,
                pacienteId: 1,
                dependenteId: null,
                planoMedicoId: 1)
        );
        Assert.Equal(expectedMessage, ex.Message);
    }


    [Fact(DisplayName = "Deve lançar exceção para MotivoConsulta nulo ou vazio")]
    public void AgendamentoConsulta_ShouldThrowException_WhenMotivoConsultaIsNullOrEmpty()
    {
        var ex = Assert.Throws<EntityValidationException>(() =>
            new AgendamentoConsulta(
                especialistaId: 1,
                especialidadeId: 1,
                localAtendimentoId: 1,
                tipoConsultaId: (int)ETipoConsulta.Presencial,
                tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
                dataConsulta: DateTime.Now.AddDays(5),
                horarioConsulta: "12:00",
                motivoConsulta: null,
                valorConsulta: null,
                telefoneContato: "1199999565",
                primeiraVez: false,
                pacienteId: 1,
                dependenteId: null,
                planoMedicoId: 1)
        );
        Assert.Equal("MotivoConsulta não pode ser nulo ou vazio.", ex.Message);
    }

    
    [Theory(DisplayName = "Deve lançar exceção para TelefoneContato inválido")]
    [InlineData("", "TelefoneContato não pode ser nulo ou vazio.")]
    [InlineData("invalid-phone", "TelefoneContato não pode ser nulo ou vazio.")] // ele remove a formatação e valida se é número
    [InlineData("12345", "TelefoneContato deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    [InlineData("123456789012", "TelefoneContato deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    public void AgendamentoConsulta_ShouldThrowException_WhenTelefoneContatoIsInvalid(string phoneValue, string expectedMessage)
    {
        var ex = Assert.Throws<EntityValidationException>(() =>
            new AgendamentoConsulta(
                especialistaId: 1,
                especialidadeId: 1,
                localAtendimentoId: 1,
                tipoConsultaId: (int)ETipoConsulta.Presencial,
                tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
                dataConsulta: DateTime.Now.AddDays(5),
                horarioConsulta: "15:30",
                motivoConsulta: _faker.Lorem.Text(),
                valorConsulta: 158.90m,
                telefoneContato: phoneValue,
                primeiraVez: false,
                pacienteId: 1,
                dependenteId: null,
                planoMedicoId: 1)
        );
        Assert.Equal(expectedMessage, ex.Message);
    }


    [Theory(DisplayName = "Deve lançar exceção para DataConsulta inválido")]
    [InlineData("2025-01-10", "DataConsulta deve ser uma data futura e maior que hoje.")]
    public void AgendamentoConsulta_ShouldThrowException_WhenDataConsultaIsInvalid(string dataConsultaStr, string expectedMessage)
    {
        // Convertendo a string para DateTime para passar para o construtor
        DateTime dataConsulta = DateTime.ParseExact(dataConsultaStr, "yyyy-MM-dd", null);

        var ex = Assert.Throws<EntityValidationException>(() =>
            new AgendamentoConsulta(
                especialistaId: 1,
                especialidadeId: 1,
                localAtendimentoId: 1,
                tipoConsultaId: (int)ETipoConsulta.Presencial,
                tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
                dataConsulta: dataConsulta,
                horarioConsulta: "15:30",
                motivoConsulta: _faker.Lorem.Text(),
                valorConsulta: 158.90m,
                telefoneContato: "11965014874",
                primeiraVez: false,
                pacienteId: 1,
                dependenteId: null,
                planoMedicoId: 1)
        );
        Assert.Equal(expectedMessage, ex.Message);
    }


    [Fact]
    public void ConfirmarPaciente_ShouldConfirm_If_StatusSolicitado_DataConsulta_IsValid_And_ConfirmedByEspecialistaAt_IsValid()
    {
        // Arrange
        var agendamento = new AgendamentoConsulta(
            especialistaId: 1,
            especialidadeId: 1,
            localAtendimentoId: 1,
            tipoConsultaId: (int)ETipoConsulta.Presencial,
            tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
            dataConsulta: DateTime.Now.AddDays(5), // Data futura
            horarioConsulta: "15:30",
            motivoConsulta: "Consulta de rotina",
            valorConsulta: 158.90m,
            telefoneContato: "11965014874",
            primeiraVez: false,
            pacienteId: 1,
            dependenteId: null,
            planoMedicoId: 1
        );

        agendamento.StatusConsultaId = (int)ETipoStatusConsulta.Solicitado;
        agendamento.ConfirmedByEspecialistaAt = DateTime.Now.AddDays(-1); // Especialista confirmou

        // Act
        agendamento.ConfirmarPaciente();

        // Assert
        agendamento.StatusConsultaId.Should().Be((int)ETipoStatusConsulta.Confirmado);
        agendamento.ConfirmedByPacienteAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1)); // Confirmado pelo paciente
        agendamento.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public void ConfirmarPaciente_ShouldNotConfirm_When_StatusSolicitadoIsInvalid()
    {
        // Arrange
        var agendamento = new AgendamentoConsulta(
            especialistaId: 1,
            especialidadeId: 1,
            localAtendimentoId: 1,
            tipoConsultaId: (int)ETipoConsulta.Presencial,
            tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
            dataConsulta: DateTime.Now.AddDays(5), // Data futura
            horarioConsulta: "15:30",
            motivoConsulta: "Consulta de rotina",
            valorConsulta: 158.90m,
            telefoneContato: "11965014874",
            primeiraVez: false,
            pacienteId: 1,
            dependenteId: null,
            planoMedicoId: 1
        );

        agendamento.StatusConsultaId = (int)ETipoStatusConsulta.Confirmado;
        agendamento.ConfirmedByEspecialistaAt = DateTime.Now.AddDays(-1); // Especialista confirmou

        // Act
        var exception = Record.Exception(() => agendamento.ConfirmarPaciente());
        // Assert
        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain("Status da Consulta inválido para confirmação");
    }

    [Fact]
    public void ConfirmarPaciente_ShouldNotConfirm_When_AlreadyConfirmedByPaciente()
    {
        // Arrange
        var agendamento = new AgendamentoConsulta(
            especialistaId: 1,
            especialidadeId: 1,
            localAtendimentoId: 1,
            tipoConsultaId: (int)ETipoConsulta.Presencial,
            tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
            dataConsulta: DateTime.Now.AddDays(5), // Data futura
            horarioConsulta: "15:30",
            motivoConsulta: "Consulta de rotina",
            valorConsulta: 158.90m,
            telefoneContato: "11965014874",
            primeiraVez: false,
            pacienteId: 1,
            dependenteId: null,
            planoMedicoId: 1
        );

        agendamento.StatusConsultaId = (int)ETipoStatusConsulta.Confirmado;
        agendamento.ConfirmedByPacienteAt = DateTime.Now.AddDays(-1); // Paciente já confirmou

        // Act
        var exception = Record.Exception(() => agendamento.ConfirmarPaciente());
        // Assert
        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain("Consulta já confirmada por você anteriormente");
    }

    [Fact]
    public void ConfirmarPaciente_ShouldNotConfirm_When_DataConsultaIsExpired()
    {
        // Arrange
        var agendamento = new AgendamentoConsulta(
            especialistaId: 1,
            especialidadeId: 1,
            localAtendimentoId: 1,
            tipoConsultaId: (int)ETipoConsulta.Presencial,
            tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
            dataConsulta: DateTime.Now.AddDays(5),
            horarioConsulta: "15:30",
            motivoConsulta: "Consulta de rotina",
            valorConsulta: 158.90m,
            telefoneContato: "11965014874",
            primeiraVez: false,
            pacienteId: 1,
            dependenteId: null,
            planoMedicoId: 1
        );

        agendamento.DataConsulta = DateTime.Now.AddDays(-5); // Data passada
        agendamento.StatusConsultaId = (int)ETipoStatusConsulta.Solicitado;
        agendamento.ConfirmedByPacienteAt = null; // Paciente NÃO confirmou        

        // Act
        var exception = Record.Exception(() => agendamento.ConfirmarPaciente());
        // Assert
        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain("Consulta cancelada automaticamente, pois não recebemos sua resposta para a confirmação em tempo hábil");
    }

    [Fact]
    public void ConfirmarPaciente_ShouldNotConfirm_When_ConfirmedByEspecialistaAtNotConfirmedYet()
    {
        // Arrange
        var agendamento = new AgendamentoConsulta(
            especialistaId: 1,
            especialidadeId: 1,
            localAtendimentoId: 1,
            tipoConsultaId: (int)ETipoConsulta.Presencial,
            tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
            dataConsulta: DateTime.Now.AddDays(5), // Data futura
            horarioConsulta: "15:30",
            motivoConsulta: "Consulta de rotina",
            valorConsulta: 158.90m,
            telefoneContato: "11965014874",
            primeiraVez: false,
            pacienteId: 1,
            dependenteId: null,
            planoMedicoId: 1
        );

        agendamento.StatusConsultaId = (int)ETipoStatusConsulta.Solicitado;
        agendamento.ConfirmedByEspecialistaAt = null; // Especialista NÃO confirmou

        // Act
        var exception = Record.Exception(() => agendamento.ConfirmarPaciente());
        // Assert
        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain("Agendamento não confirmado pelo especialista");
    }



    [Fact]
    public void ConfirmarProfissional_ShouldConfirm_When_StatusConsultaIdIsValid_And_DataConsultaIsValid()
    {
        // Arrange
        var agendamento = new AgendamentoConsulta(
            especialistaId: 1,
            especialidadeId: 1,
            localAtendimentoId: 1,
            tipoConsultaId: (int)ETipoConsulta.Presencial,
            tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
            dataConsulta: DateTime.Now.AddDays(5), // Data futura
            horarioConsulta: "15:30",
            motivoConsulta: "Consulta de rotina",
            valorConsulta: 158.90m,
            telefoneContato: "11965014874",
            primeiraVez: false,
            pacienteId: 1,
            dependenteId: null,
            planoMedicoId: 1
        );

        agendamento.StatusConsultaId = (int)ETipoStatusConsulta.Solicitado;
        agendamento.ConfirmedByEspecialistaAt = null; // Especialista NÃO confirmou

        // Act & Assert
        FluentActions.Invoking(() => agendamento.ConfirmarProfissional())
            .Should().NotThrow();

        agendamento.ConfirmedByEspecialistaAt.Should().NotBeNull();
        agendamento.ConfirmedByEspecialistaAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1)); // Confirmado pelo especialista
        agendamento.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public void ConfirmarProfissional_ShouldNotConfirm_When_AlreadyConfirmedByEspecialista()
    {
        // Arrange
        var agendamento = new AgendamentoConsulta(
            especialistaId: 1,
            especialidadeId: 1,
            localAtendimentoId: 1,
            tipoConsultaId: (int)ETipoConsulta.Presencial,
            tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
            dataConsulta: DateTime.Now.AddDays(5), // Data futura
            horarioConsulta: "15:30",
            motivoConsulta: "Consulta de rotina",
            valorConsulta: 158.90m,
            telefoneContato: "11965014874",
            primeiraVez: false,
            pacienteId: 1,
            dependenteId: null,
            planoMedicoId: 1
        );

        agendamento.StatusConsultaId = (int)ETipoStatusConsulta.Solicitado;
        agendamento.ConfirmedByEspecialistaAt = DateTime.Now.AddDays(2); // Especialista JÁ confirmou

        // Act
        var exception = Record.Exception(() => agendamento.ConfirmarProfissional());
        // Assert
        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain("Consulta já confirmada por você anteriormente");
    }

    [Fact]
    public void ConfirmarProfissional_ShouldNotConfirm_When_StatusConsultaIdIsInvalid()
    {
        // Arrange
        var agendamento = new AgendamentoConsulta(
            especialistaId: 1,
            especialidadeId: 1,
            localAtendimentoId: 1,
            tipoConsultaId: (int)ETipoConsulta.Presencial,
            tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
            dataConsulta: DateTime.Now.AddDays(5), // Data futura
            horarioConsulta: "15:30",
            motivoConsulta: "Consulta de rotina",
            valorConsulta: 158.90m,
            telefoneContato: "11965014874",
            primeiraVez: false,
            pacienteId: 1,
            dependenteId: null,
            planoMedicoId: 1
        );

        agendamento.StatusConsultaId = (int)ETipoStatusConsulta.Confirmado;
        agendamento.ConfirmedByEspecialistaAt = null; // Especialista NÃO confirmou

        // Act
        var exception = Record.Exception(() => agendamento.ConfirmarProfissional());
        // Assert
        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain("Status da Consulta inválido para confirmação");
    }

    [Fact]
    public void ConfirmarProfissional_ShouldNotConfirm_When_DataConsultaIsExpired()
    {
        // Arrange
        var agendamento = new AgendamentoConsulta(
            especialistaId: 1,
            especialidadeId: 1,
            localAtendimentoId: 1,
            tipoConsultaId: (int)ETipoConsulta.Presencial,
            tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
            dataConsulta: DateTime.Now.AddDays(5),
            horarioConsulta: "15:30",
            motivoConsulta: "Consulta de rotina",
            valorConsulta: 158.90m,
            telefoneContato: "11965014874",
            primeiraVez: false,
            pacienteId: 1,
            dependenteId: null,
            planoMedicoId: 1
        );

        agendamento.DataConsulta = DateTime.Now.AddDays(-5); // Data passada
        agendamento.StatusConsultaId = (int)ETipoStatusConsulta.Solicitado;
        agendamento.ConfirmedByEspecialistaAt = null; // Especialista NÃO confirmou

        // Act
        var exception = Record.Exception(() => agendamento.ConfirmarProfissional());
        // Assert
        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain("Data da Consulta inválido para confirmação");
    }



    [Fact]
    public void Cancelar_ShouldCancel_When_StatusConsultaIdIsValid_And_DataConsultaIsValid()
    {
        // Arrange
        var notaCancelamento = _faker.Lorem.Text();

        var agendamento = new AgendamentoConsulta(
            especialistaId: 1,
            especialidadeId: 1,
            localAtendimentoId: 1,
            tipoConsultaId: (int)ETipoConsulta.Presencial,
            tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
            dataConsulta: DateTime.Now.AddDays(5),
            horarioConsulta: "15:30",
            motivoConsulta: "Consulta de rotina",
            valorConsulta: 158.90m,
            telefoneContato: "11965014874",
            primeiraVez: false,
            pacienteId: 1,
            dependenteId: null,
            planoMedicoId: 1
        );

        agendamento.StatusConsultaId = (int)ETipoStatusConsulta.Solicitado;

        // Act & Assert
        FluentActions.Invoking(() => agendamento.Cancelar(notaCancelamento))
            .Should().NotThrow();

        agendamento.NotaCancelamento.Should().NotBeNull();
        agendamento.NotaCancelamento.Should().Be(notaCancelamento);
        agendamento.StatusConsultaId.Should().Be((int)ETipoStatusConsulta.Cancelado);
        agendamento.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public void Cancelar_ShouldNotCancel_When_StatusConsultaIdIsInvalid()
    {
        // Arrange
        var agendamento = new AgendamentoConsulta(
            especialistaId: 1,
            especialidadeId: 1,
            localAtendimentoId: 1,
            tipoConsultaId: (int)ETipoConsulta.Presencial,
            tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
            dataConsulta: DateTime.Now.AddDays(5),
            horarioConsulta: "15:30",
            motivoConsulta: "Consulta de rotina",
            valorConsulta: 158.90m,
            telefoneContato: "11965014874",
            primeiraVez: false,
            pacienteId: 1,
            dependenteId: null,
            planoMedicoId: 1
        );
        
        agendamento.StatusConsultaId = (int)ETipoStatusConsulta.Atendido; // Não cancela uma consulta JÁ atendida

        // Act
        var exception = Record.Exception(() => agendamento.Cancelar(notaCancelamento: _faker.Lorem.Text()));
        // Assert
        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain("Status da Consulta inválido para cancelamento");
    }

    [Fact]
    public void Cancelar_ShouldNotCancel_When_DataConsultaIsExpired()
    {
        // Arrange
        var agendamento = new AgendamentoConsulta(
            especialistaId: 1,
            especialidadeId: 1,
            localAtendimentoId: 1,
            tipoConsultaId: (int)ETipoConsulta.Presencial,
            tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
            dataConsulta: DateTime.Now.AddDays(5),
            horarioConsulta: "15:30",
            motivoConsulta: "Consulta de rotina",
            valorConsulta: 158.90m,
            telefoneContato: "11965014874",
            primeiraVez: false,
            pacienteId: 1,
            dependenteId: null,
            planoMedicoId: 1
        );
        agendamento.DataConsulta = DateTime.Now.AddDays(-15); // Não cancela uma consulta com data passada pois ela sera expirada e cancelada automaticamente

        // Act
        var exception = Record.Exception(() => agendamento.Cancelar(notaCancelamento: _faker.Lorem.Text()));
        // Assert
        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain("Data da Consulta inválido para cancelamento");
    }

    [Fact]
    public void Cancelar_ShouldNotCancel_When_NotaCancelamentoIsNullOrEmpty()
    {
        // Arrange
        var agendamento = new AgendamentoConsulta(
            especialistaId: 1,
            especialidadeId: 1,
            localAtendimentoId: 1,
            tipoConsultaId: (int)ETipoConsulta.Presencial,
            tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
            dataConsulta: DateTime.Now.AddDays(5),
            horarioConsulta: "15:30",
            motivoConsulta: "Consulta de rotina",
            valorConsulta: 158.90m,
            telefoneContato: "11965014874",
            primeiraVez: false,
            pacienteId: 1,
            dependenteId: null,
            planoMedicoId: 1
        );

        // Act
        var exception = Record.Exception(() => agendamento.Cancelar(notaCancelamento: null));
        // Assert
        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain("NotaCancelamento não pode ser nulo ou vazio.");
    }


    [Fact]
    public void ExpirarProfissional_ShouldExpire_When_NotaCancelamentoIsValid()
    {
        // Arrange
        var notaCancelamento = _faker.Lorem.Text();

        var agendamento = new AgendamentoConsulta(
            especialistaId: 1,
            especialidadeId: 1,
            localAtendimentoId: 1,
            tipoConsultaId: (int)ETipoConsulta.Presencial,
            tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
            dataConsulta: DateTime.Now.AddDays(5),
            horarioConsulta: "15:30",
            motivoConsulta: "Consulta de rotina",
            valorConsulta: 158.90m,
            telefoneContato: "11965014874",
            primeiraVez: false,
            pacienteId: 1,
            dependenteId: null,
            planoMedicoId: 1
        );

        agendamento.StatusConsultaId = (int)ETipoStatusConsulta.Solicitado;

        // Act & Assert
        FluentActions.Invoking(() => agendamento.ExpirarProfissional(notaCancelamento))
            .Should().NotThrow();

        agendamento.NotaCancelamento.Should().NotBeNull();
        agendamento.NotaCancelamento.Should().Be(notaCancelamento);
        agendamento.StatusConsultaId.Should().Be((int)ETipoStatusConsulta.ExpiradoProfissional);
        agendamento.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public void ExpirarProfissional_ShouldNotExpire_When_NotaCancelamentoIsNullOrEmpty()
    {
        // Arrange
        var agendamento = new AgendamentoConsulta(
            especialistaId: 1,
            especialidadeId: 1,
            localAtendimentoId: 1,
            tipoConsultaId: (int)ETipoConsulta.Presencial,
            tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
            dataConsulta: DateTime.Now.AddDays(5),
            horarioConsulta: "15:30",
            motivoConsulta: "Consulta de rotina",
            valorConsulta: 158.90m,
            telefoneContato: "11965014874",
            primeiraVez: false,
            pacienteId: 1,
            dependenteId: null,
            planoMedicoId: 1
        );

        // Act
        var exception = Record.Exception(() => agendamento.ExpirarProfissional(notaCancelamento: null));
        // Assert
        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain("NotaCancelamento não pode ser nulo ou vazio.");
    }


    [Fact]
    public void ExpirarPaciente_ShouldExpire_When_NotaCancelamentoIsValid()
    {
        // Arrange
        var notaCancelamento = _faker.Lorem.Text();

        var agendamento = new AgendamentoConsulta(
            especialistaId: 1,
            especialidadeId: 1,
            localAtendimentoId: 1,
            tipoConsultaId: (int)ETipoConsulta.Presencial,
            tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
            dataConsulta: DateTime.Now.AddDays(5),
            horarioConsulta: "15:30",
            motivoConsulta: "Consulta de rotina",
            valorConsulta: 158.90m,
            telefoneContato: "11965014874",
            primeiraVez: false,
            pacienteId: 1,
            dependenteId: null,
            planoMedicoId: 1
        );

        agendamento.StatusConsultaId = (int)ETipoStatusConsulta.Solicitado;

        // Act & Assert
        FluentActions.Invoking(() => agendamento.ExpirarPaciente(notaCancelamento))
            .Should().NotThrow();

        agendamento.NotaCancelamento.Should().NotBeNull();
        agendamento.NotaCancelamento.Should().Be(notaCancelamento);
        agendamento.StatusConsultaId.Should().Be((int)ETipoStatusConsulta.ExpiradoPaciente);
        agendamento.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public void ExpirarPaciente_ShouldNotExpire_When_NotaCancelamentoIsNullOrEmpty()
    {
        // Arrange
        var agendamento = new AgendamentoConsulta(
            especialistaId: 1,
            especialidadeId: 1,
            localAtendimentoId: 1,
            tipoConsultaId: (int)ETipoConsulta.Presencial,
            tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
            dataConsulta: DateTime.Now.AddDays(5),
            horarioConsulta: "15:30",
            motivoConsulta: "Consulta de rotina",
            valorConsulta: 158.90m,
            telefoneContato: "11965014874",
            primeiraVez: false,
            pacienteId: 1,
            dependenteId: null,
            planoMedicoId: 1
        );

        // Act
        var exception = Record.Exception(() => agendamento.ExpirarPaciente(notaCancelamento: null));
        // Assert
        exception.Should().BeOfType<EntityValidationException>();
        exception.Message.Should().Contain("NotaCancelamento não pode ser nulo ou vazio.");
    }
}