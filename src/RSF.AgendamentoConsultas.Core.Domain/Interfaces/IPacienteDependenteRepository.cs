using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Common;

namespace RSF.AgendamentoConsultas.Core.Domain.Interfaces;

public interface IPacienteDependenteRepository : IBaseRepository<PacienteDependente>
{
    new ValueTask<PacienteDependente> GetByIdAsync(int dependenteId);
}