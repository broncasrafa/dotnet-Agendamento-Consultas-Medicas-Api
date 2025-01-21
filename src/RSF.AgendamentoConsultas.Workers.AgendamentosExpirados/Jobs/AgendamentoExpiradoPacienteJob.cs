using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Enums;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;

namespace RSF.AgendamentoConsultas.Workers.AgendamentosExpirados.Jobs;

public class AgendamentoExpiradoPacienteJob : IHostedService
{
    private readonly ILogger<AgendamentoExpiradoPacienteJob> _logger;
    private readonly IServiceProvider _serviceProvider;

    public AgendamentoExpiradoPacienteJob(ILogger<AgendamentoExpiradoPacienteJob> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }


    public async Task ProcessTaskAsync(PerformContext? context)
    {
        using var scope = _serviceProvider.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<IAgendamentoConsultaRepository>();

        var agendamentosExpirados = await repository.GetAllExpiredByPacienteAsync();

        if (agendamentosExpirados is not null && agendamentosExpirados.Any())
        {
            agendamentosExpirados.ToList().ForEach(x =>
            {
                x.StatusConsultaId = (int)ETipoStatusConsulta.ExpiradoPaciente;
                x.NotaCancelamento = "Consulta cancelada automaticamente, pois não recebemos sua resposta para a confirmação em tempo hábil.";
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

        RecurringJob.AddOrUpdate("worker-agendamento-expirado-paciente", () => ProcessTaskAsync(null), "0 * * * *"); // "*/1 * * * *"

        _logger.LogInformation("Tarefa agendada com sucesso!");

        await Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}