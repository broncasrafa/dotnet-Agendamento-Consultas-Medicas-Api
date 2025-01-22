using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Common;

namespace RSF.AgendamentoConsultas.Core.Domain.Interfaces;

public interface IPacienteRepository : IBaseRepository<Paciente>
{
    ValueTask<Paciente> GetByIdDetailsAsync(int pacienteId);
    ValueTask<IReadOnlyList<PacientePlanoMedico>> GetPlanosMedicosPacienteByIdAsync(int pacienteId);
    ValueTask<IReadOnlyList<PacienteDependente>> GetDependentesPacienteByIdAsync(int pacienteId);
    ValueTask<IReadOnlyList<EspecialistaAvaliacao>> GetAvaliacoesPacienteByIdAsync(int pacienteId);
}