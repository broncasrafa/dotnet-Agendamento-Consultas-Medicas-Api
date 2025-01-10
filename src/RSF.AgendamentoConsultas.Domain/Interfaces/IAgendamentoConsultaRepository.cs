using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;

namespace RSF.AgendamentoConsultas.Domain.Interfaces;

public interface IAgendamentoConsultaRepository : IBaseRepository<AgendamentoConsulta>
{
    ValueTask<IReadOnlyList<AgendamentoConsulta>> GetAllByPacienteIdAsync(int pacienteId);
    new ValueTask<AgendamentoConsulta> GetByIdAsync(int id);
}