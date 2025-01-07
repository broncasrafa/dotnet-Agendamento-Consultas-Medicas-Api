using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using RSF.AgendamentoConsultas.Shareable.Results;

namespace RSF.AgendamentoConsultas.Domain.Interfaces;

public interface IEspecialistaRepository : IBaseRepository<Especialista>
{
    ValueTask<Especialista> GetByIdWithEspecialidadesAsync(int id);
    ValueTask<Especialista> GetByIdWithConveniosMedicosAsync(int id);
    ValueTask<Especialista> GetByIdWithAvaliacoesAsync(int id);
    ValueTask<Especialista> GetByIdWithLocaisAtendimentoAsync(int id);
    ValueTask<Especialista> GetByIdWithTagsAsync(int id);
    ValueTask<Especialista> GetByIdWithPerguntasRespostasAsync(int id);
    ValueTask<PagedResult<Especialista>> GetAllPagedAsync(int pageNumber = 1, int pageSize = 10);
    ValueTask<PagedResult<Especialista>> GetAllByNamePagedAsync(string name, int pageNumber = 1, int pageSize = 10);
}