﻿using Microsoft.Extensions.Logging;
using RSF.AgendamentoConsultas.Domain.Events;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Domain.Notifications;
using RSF.AgendamentoConsultas.Shareable.Extensions;


namespace RSF.AgendamentoConsultas.Application.Features.Pergunta.EventHandler;

#pragma warning disable CA1711
public sealed class PerguntaEventHandler : IEventHandler<PerguntaCreatedEvent>
{
    private readonly ILogger<PerguntaEventHandler> _logger;
    private readonly IMailSender _mailSender;
    private readonly IEspecialistaRepository _especialistaRepository;

    public PerguntaEventHandler(
        ILogger<PerguntaEventHandler> logger,
        IMailSender mailSender,
        IEspecialistaRepository especialistaRepository)
    {
        _logger = logger;
        _mailSender = mailSender;
        _especialistaRepository = especialistaRepository;
    }

    public async Task Handle(PerguntaCreatedEvent @event)
    {
        _logger.LogInformation("[{DataAtual}] Consumindo a mensagem da fila: 'PerguntaCreatedEvent' - {Event}", DateTime.Now, @event.ToJson(false));

        var especialistas = await _especialistaRepository.GetAllByEspecialidadeIdAsync(@event.EspecialidadeId);

        foreach (var esp in especialistas)
        {
            var body = $@"
            Caro profissional {esp.Nome},

            O paciente {@event.PacienteNome} realizou a seguinte pergunta para os profissionais da especialidade '{@event.EspecialidadeNome}':

            {@event.Pergunta}

            Para responder clique no link: http://frontend/resposta/?perguntaId={@event.PerguntaId}&espId={esp.EspecialistaId}


            Só agradece !!!

            Atenciosamente
            Agendamento de Consultas Médicas";

            await _mailSender.SendMailAsync(new MailTo(esp.Nome, esp.Email), "Sua especialidade recebeu uma nova pergunta", body);
        }

        await Task.CompletedTask;
    }
}