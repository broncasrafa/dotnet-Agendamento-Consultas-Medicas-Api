using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;

namespace RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

public interface IPacienteDependenteRepository : IBaseRepository<PacienteDependente>
{
    new ValueTask<PacienteDependente> GetByIdAsync(int dependenteId);
}