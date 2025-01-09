using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;

namespace RSF.AgendamentoConsultas.Domain.Interfaces;

public interface IPacienteDependenteRepository : IBaseRepository<PacienteDependente>
{
    ValueTask<PacienteDependente> GetByIdAsync(int dependenteId, int pacientePrincipalId);
}