﻿using Microsoft.Extensions.Configuration;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.CreateAgendamento;

public class CreateAgendamentoRequestHandler : IRequestHandler<CreateAgendamentoRequest, Result<int>>
{
    private readonly IAgendamentoConsultaRepository _agendamentoConsultaRepository;
    private readonly IEspecialistaRepository _especialistaRepository;
    private readonly IBaseRepository<Domain.Entities.TipoConsulta> _tipoConsultaRepository;
    private readonly IBaseRepository<Domain.Entities.TipoAgendamento> _tipoAgendamentoRepository;
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IEventBus _eventBus;
    private readonly IConfiguration _configuration;

    public CreateAgendamentoRequestHandler(
        IAgendamentoConsultaRepository agendamentoConsultaRepository,
        IEspecialistaRepository especialistaRepository,
        IBaseRepository<Domain.Entities.TipoConsulta> tipoConsultaRepository,
        IBaseRepository<Domain.Entities.TipoAgendamento> tipoAgendamentoRepository,
        IPacienteRepository pacienteRepository,
        IEventBus eventBus,
        IConfiguration configuration)
    {
        _agendamentoConsultaRepository = agendamentoConsultaRepository;
        _especialistaRepository = especialistaRepository;
        _tipoConsultaRepository = tipoConsultaRepository;
        _tipoAgendamentoRepository = tipoAgendamentoRepository;
        _pacienteRepository = pacienteRepository;
        _eventBus = eventBus;
        _configuration = configuration;
    }

    public async Task<Result<int>> Handle(CreateAgendamentoRequest request, CancellationToken cancellationToken)
    {
        await ValidateAsync(request);

        var newAgendamento = new AgendamentoConsulta(
            especialistaId: request.EspecialistaId,
            especialidadeId: request.EspecialidadeId,
            localAtendimentoId: request.LocalAtendimentoId,
            tipoConsultaId: request.TipoConsultaId,
            tipoAgendamentoId: request.TipoAgendamentoId,
            dataConsulta: request.DataConsulta,
            horarioConsulta: request.HorarioConsulta,
            motivoConsulta: request.MotivoConsulta,
            valorConsulta: request.ValorConsulta,
            telefoneContato: request.TelefoneContato,
            primeiraVez: request.PrimeiraVez,
            pacienteId: request.PacienteId,
            dependenteId: request.DependenteId,
            planoMedicoId: request.PlanoMedicoId);

        var rowsAffected = await _agendamentoConsultaRepository.AddAsync(newAgendamento);

        var agendamento = await _agendamentoConsultaRepository.GetByIdAsync(newAgendamento.AgendamentoConsultaId);

        // pode disparar uma mensagem para a fila de AgendamentoConsultas (o especialista recebe um email de uma nova consulta para ele(onde ele pode aceitar ou recusar, propor um novo horario)
        var @event = new AgendamentoCreatedEvent
        (
            agendamentoConsultaId: agendamento.AgendamentoConsultaId,
            dataAgendamento: agendamento.CreatedAt.ToShortDateString(),
            tipoAgendamento: agendamento.TipoAgendamento.Descricao,
            tipoConsulta: agendamento.TipoConsulta.Descricao,
            especialidade: agendamento.Especialidade.NomePlural,
            especialista: agendamento.Especialista.Nome,
            especialistaEmail: agendamento.Especialista.Email,
            paciente: agendamento.Paciente.Nome,
            convenioMedico: agendamento.PlanoMedico?.ConvenioMedico?.Nome ?? agendamento.PlanoMedicoDependente?.ConvenioMedico?.Nome,
            motivoConsulta: agendamento.MotivoConsulta,
            dataConsulta: agendamento.DataConsulta.ToShortDateString(),
            horarioConsulta: agendamento.HorarioConsulta,
            primeiraVez: agendamento.PrimeiraVez ?? false,
            localAtendimentoNome: agendamento.LocalAtendimento.Nome,
            localAtendimentoLogradouro: agendamento.LocalAtendimento.Logradouro,
            localAtendimentoComplemento: agendamento.LocalAtendimento.Complemento,
            localAtendimentoBairro: agendamento.LocalAtendimento.Bairro,
            localAtendimentoCep: agendamento.LocalAtendimento.Cep,
            localAtendimentoCidade: agendamento.LocalAtendimento.Cidade,
            localAtendimentoEstado: agendamento.LocalAtendimento.Estado
        );

        _eventBus.Publish(@event, _configuration.GetSection("RabbitMQ:AgendamentoQueueName").Value);

        return await Task.FromResult(rowsAffected);
    }



    private async Task ValidateAsync(CreateAgendamentoRequest request)
    {
        var especialista = await _especialistaRepository.GetByIdAsync(request.EspecialistaId);
        NotFoundException.ThrowIfNull(especialista, $"Especialista com o ID: '{request.EspecialistaId}' não encontrado");

        var especialidade = especialista.Especialidades.SingleOrDefault(c => c.EspecialidadeId == request.EspecialidadeId);
        NotFoundException.ThrowIfNull(especialidade, $"Especialidade com o ID: '{request.EspecialidadeId}' não encontrado para o especialista ID: '{request.EspecialistaId}'");

        var localAtendimento = especialista.LocaisAtendimento.SingleOrDefault(c => c.Id == request.LocalAtendimentoId);
        NotFoundException.ThrowIfNull(localAtendimento, $"Local de atendimento com o ID: '{request.LocalAtendimentoId}' não encontrado para o especialista ID: '{request.EspecialistaId}'");

        NotFoundException.ThrowIfNull(await _tipoConsultaRepository.GetByIdAsync(request.TipoConsultaId), $"Tipo de Consulta com o ID: '{request.TipoConsultaId}' não encontrada");

        NotFoundException.ThrowIfNull(await _tipoAgendamentoRepository.GetByIdAsync(request.TipoAgendamentoId), $"Tipo de Agendamento com o ID: '{request.TipoAgendamentoId}' não encontrado");


        var paciente = await _pacienteRepository.GetByIdDetailsAsync(request.PacienteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        if (request.DependenteId is not null)
        {
            var dependente = paciente.Dependentes.SingleOrDefault(c => c.DependenteId == request.DependenteId);
            NotFoundException.ThrowIfNull(dependente, $"Dependente com o ID: '{request.DependenteId}' não encontrado para o paciente ID: '{request.PacienteId}'");

            var dependentePlanoMedico = dependente!.PlanosMedicos.SingleOrDefault(c => c.PlanoMedicoId == request.PlanoMedicoId);
            NotFoundException.ThrowIfNull(dependentePlanoMedico, $"Plano Medico com o ID: '{request.PlanoMedicoId}' do dependente ID: '{request.DependenteId}' não encontrado");
        }
        else
        {
            var planoMedicoPacientePrincipal = paciente.PlanosMedicos.SingleOrDefault(c => c.PlanoMedicoId == request.PlanoMedicoId);
            NotFoundException.ThrowIfNull(planoMedicoPacientePrincipal, $"Plano Medico com o ID: '{request.PlanoMedicoId}' do paciente ID: '{request.PacienteId}' não encontrado");
        }
    }
}