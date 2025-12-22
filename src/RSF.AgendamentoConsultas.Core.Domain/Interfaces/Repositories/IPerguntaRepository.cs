using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;

namespace RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

public interface IPerguntaRepository : IBaseRepository<Pergunta>
{
    new ValueTask<Pergunta> GetByIdAsync(int id);
    ValueTask<Pergunta> GetByIdAsync(int perguntaId, int especialidadeId);
    ValueTask<PagedResult<Pergunta>> GetAllPagedAsync(int pageNumber, int pageSize);
    ValueTask<PagedResult<Pergunta>> GetByListaPerguntaIdsPagedAsync(List<int> perguntaIds, int pageNumber, int pageSize);
}