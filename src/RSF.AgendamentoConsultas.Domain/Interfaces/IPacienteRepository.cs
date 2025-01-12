using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;

namespace RSF.AgendamentoConsultas.Domain.Interfaces;

public interface IPacienteRepository : IBaseRepository<Paciente>
{
    ValueTask<Paciente> GetByIdDetailsAsync(int pacienteId);
    ValueTask<IReadOnlyList<PacientePlanoMedico>> GetPlanosMedicosPacienteByIdAsync(int pacienteId);
    ValueTask<IReadOnlyList<PacienteDependente>> GetDependentesPacienteByIdAsync(int pacienteId);
    ValueTask<IReadOnlyList<EspecialistaAvaliacao>> GetAvaliacoesPacienteByIdAsync(int pacienteId);
}