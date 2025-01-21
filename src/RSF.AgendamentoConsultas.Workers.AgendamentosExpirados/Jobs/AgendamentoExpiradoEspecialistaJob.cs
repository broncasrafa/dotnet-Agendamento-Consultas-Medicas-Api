using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Enums;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;

namespace RSF.AgendamentoConsultas.Workers.AgendamentosExpirados.Jobs;

public class AgendamentoExpiradoEspecialistaJob : IHostedService
{
    private readonly ILogger<AgendamentoExpiradoEspecialistaJob> _logger;
    private readonly IServiceProvider _serviceProvider;

    public AgendamentoExpiradoEspecialistaJob(ILogger<AgendamentoExpiradoEspecialistaJob> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }


    public async Task ProcessTaskAsync(PerformContext? context)
    {
        using var scope = _serviceProvider.CreateScope();

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