using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;

namespace RSF.AgendamentoConsultas.Domain.Interfaces;

public interface IConvenioMedicoRepository : IBaseRepository<ConvenioMedico>
{
    ValueTask<ConvenioMedico> GetByIdWithCidadesAtendidasAsync(int id);
    ValueTask<ConvenioMedico> GetByIdWithCidadesAtendidasAsync(int id, int estadoId);
}