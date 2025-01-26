using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;

namespace RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

public interface IPacienteRepository : IBaseRepository<Paciente>
{
    ValueTask<Paciente> GetByIdDetailsAsync(int pacienteId);
    ValueTask<Paciente> GetByEmailAsync(string email);
    ValueTask<Paciente> GetByUserIdAsync(string userId);
    ValueTask<IReadOnlyList<PacientePlanoMedico>> GetPlanosMedicosPacienteByIdAsync(int pacienteId);
    ValueTask<IReadOnlyList<PacienteDependente>> GetDependentesPacienteByIdAsync(int pacienteId);
    ValueTask<IReadOnlyList<EspecialistaAvaliacao>> GetAvaliacoesPacienteByIdAsync(int pacienteId);
}