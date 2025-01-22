using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.Infra.MessageBroker.Configurations;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using Microsoft.Extensions.Options;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Workers.AgendamentosExpirados.Jobs;

public class AgendamentoExpiradoEspecialistaJob : IHostedService
{
    private readonly ILogger<AgendamentoExpiradoEspecialistaJob> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IOptions<RabbitMQSettings> _options;

    public AgendamentoExpiradoEspecialistaJob(
        ILogger<AgendamentoExpiradoEspecialistaJob> logger, 
        IServiceProvider serviceProvider, 
        IOptions<RabbitMQSettings> options)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _options = options;
    }


    public async Task ProcessTaskAsync(PerformContext? context)
    {
        using var scope = _serviceProvider.CreateScope();
        var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
        var repository = scope.ServiceProvider.GetRequiredService<IAgendamentoConsultaRepository>();

        var agendamentosExpirados = await repository.GetAllExpiredByEspecialistaAsync();

        if (agendamentosExpirados is not null && agendamentosExpirados.Any())
        {
            agendamentosExpirados.ToList().ForEach(x => {
                x.StatusConsultaId = (int)ETipoStatusConsulta.ExpiradoProfissional;
                x.NotaCancelamento = $"Consulta cancelada automaticamente, pois não recebemos a resposta de {x.Especialista.Nome} em tempo hábil.";
                x.UpdatedAt = DateTime.Now;
            });

            var rowsAffected = await repository.UpdateRangeAsync(agendamentosExpirados);

            agendamentosExpirados.ToList().ForEach(x => { 
                var @event = new AgendamentoExpiredByEspecialistaEvent
                (
                    x.Paciente.Nome,
                    x.Especialista.Nome,
                    x.Especialista.Email,
                    x.Especialidade.Nome,
                    x.DataConsulta.ToString("dd/MM/yyyy"),
                    x.HorarioConsulta,
                    x.LocalAtendimento.Nome
                );
                eventBus.Publish(@event, _options.Value.AgendamentoExpiradoEspecialistaQueueName);
            });

            context.WriteLine($"Agendamentos expirados: {rowsAffected}");
        } 
        else
        {
            context.WriteLine($"Agendamentos expirados: 0");
        }        
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Agendando tarefa...");

        RecurringJob.AddOrUpdate("worker-agendamento-expirado-especialista", () => ProcessTaskAsync(null), "0 * * * *"); // "*/1 * * * *"

        _logger.LogInformation("Tarefa agendada com sucesso!");

        await Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}