using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;

namespace RSF.AgendamentoConsultas.Domain.Interfaces;

public interface IAgendamentoConsultaRepository : IBaseRepository<AgendamentoConsulta>
{
    new ValueTask<AgendamentoConsulta> GetByIdAsync(int agendamentoId);
    ValueTask<AgendamentoConsulta> GetByIdAsync(int agendamentoId, int pacienteId);
    ValueTask<AgendamentoConsulta> GetByIdAsync(int agendamentoId, int pacienteId, int dependenteId);
    ValueTask<IReadOnlyList<AgendamentoConsulta>> GetAllByPacienteIdAsync(int pacienteId);

    ValueTask<IReadOnlyList<AgendamentoConsulta>> GetAllExpiredByPacienteAsync();
    ValueTask<IReadOnlyList<AgendamentoConsulta>> GetAllExpiredByEspecialistaAsync();
}