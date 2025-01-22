using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;

namespace RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

public interface IConvenioMedicoRepository : IBaseRepository<ConvenioMedico>
{
    ValueTask<ConvenioMedico> GetByIdWithCidadesAtendidasAsync(int id);
    ValueTask<ConvenioMedico> GetByIdWithCidadesAtendidasAsync(int id, int estadoId);
}