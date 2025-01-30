using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.Infra.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Infra.Notifications.Templates;
using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Consumers.Notification.Subscribers;

public class DesativarContaSubscriber : RabbitMQConsumerBase
{
    private readonly ILogger<DesativarContaSubscriber> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _queueName;

    public DesativarContaSubscriber(ILogger<DesativarContaSubscriber> logger, IOptions<RabbitMQSettings> options, IServiceProvider serviceProvider)
        : base(logger, options, options.Value.DeactivateAccountQueueName)
    {
        _logger = logger;
        _queueName = options.Value.DeactivateAccountQueueName;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ProcessMessageAsync(string message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Consuming message from queue '{QueueName}'", _queueName);

        using var scope = _serviceProvider.CreateScope();

        var mailSender = scope.ServiceProvider.GetRequiredService<DesativarContaEmail>();

        var @event = JsonSerializer.Deserialize<DesativarContaCreatedEvent>(message);

        if (@event.Paciente is not null)
        {
            var pacienteRepository = scope.ServiceProvider.GetRequiredService<IPacienteRepository>();
            var pacientePlanoMedicoRepository = scope.ServiceProvider.GetRequiredService<IPacientePlanoMedicoRepository>();
            var dependenteRepository = scope.ServiceProvider.GetRequiredService<IPacienteDependenteRepository>();
            var dependentePlanoMedicoRepository = scope.ServiceProvider.GetRequiredService<IPacienteDependentePlanoMedicoRepository>();
            var agendamentoRepository = scope.ServiceProvider.GetRequiredService<IAgendamentoConsultaRepository>();

            var paciente = @event.Paciente;
            paciente.Ativo = false;
            paciente.UpdatedAt = DateTime.Now;
            await pacienteRepository.UpdateAsync(paciente);

            var planosMedicos = paciente.PlanosMedicos;
            if (planosMedicos is not null && planosMedicos.Any())
            {
                planosMedicos.ToList().ForEach(c => c.Ativo = false);
                await pacientePlanoMedicoRepository.UpdateRangeAsync(planosMedicos);
            }

            var dependentes = paciente.Dependentes;
            if (dependentes is not null && dependentes.Any())
            {
                foreach (var dep in dependentes)
                {
                    dep.Ativo = false;
                    dep.UpdatedAt = DateTime.Now;
                    if (dep.PlanosMedicos is not null && dep.PlanosMedicos.Any())
                    {
                        dep.PlanosMedicos.ToList().ForEach(c => c.Ativo = false);
                        await dependentePlanoMedicoRepository.UpdateRangeAsync(dep.PlanosMedicos);
                    }
                }
                await dependenteRepository.UpdateRangeAsync(dependentes);
            }

            var agendamentos = @event.Paciente.AgendamentosRealizados;
            if (agendamentos is not null && agendamentos.Any())
            {
                foreach (var item in agendamentos)
                {
                    item.StatusConsultaId = (int)ETipoStatusConsulta.Cancelado;
                    item.NotaCancelamento = "Consulta cancelada automaticamente. Paciente não faz mais parte do quadro de pacientes da plataforma.";
                    item.UpdatedAt = DateTime.Now;
                }
                await agendamentoRepository.UpdateRangeAsync(agendamentos);
            }
        }
        else
        {
            var especialistaRepository = scope.ServiceProvider.GetRequiredService<IEspecialistaRepository>();
            var especialista = @event.Especialista;
            especialista.Ativo = false;
            await especialistaRepository.UpdateAsync(especialista);

            var agendamentoRepository = scope.ServiceProvider.GetRequiredService<IAgendamentoConsultaRepository>();
            var agendamentos = @event.Especialista.ConsultasAtendidas;
            if (agendamentos is not null && agendamentos.Any())
            {
                foreach (var item in agendamentos)
                {
                    item.StatusConsultaId = (int)ETipoStatusConsulta.Cancelado;
                    item.NotaCancelamento = "Consulta cancelada automaticamente. Profissional não faz mais parte do quadro de especialistas disponíveis para atendimento.";
                    item.UpdatedAt = DateTime.Now;
                }
                await agendamentoRepository.UpdateRangeAsync(agendamentos);
            }
        }

        var nome = @event.Paciente?.Nome ?? @event.Especialista?.Nome;
        var email = @event.Paciente?.Email ?? @event.Especialista?.Email;

        await mailSender.SendEmailAsync(new MailTo(nome, email), nome);

        await Task.CompletedTask;
    }
}