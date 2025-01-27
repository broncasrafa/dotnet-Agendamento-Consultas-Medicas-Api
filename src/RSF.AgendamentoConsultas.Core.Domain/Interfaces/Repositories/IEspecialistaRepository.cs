using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;

namespace RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

public interface IEspecialistaRepository : IBaseRepository<Especialista>
{
    new ValueTask<Especialista> GetByIdAsync(int id);
    ValueTask<Especialista> GetByEmailAsync(string email);
    ValueTask<Especialista> GetByUserIdAsync(string userId);
    ValueTask<Especialista> GetByIdWithEspecialidadesAsync(int id);
    ValueTask<Especialista> GetByIdWithConveniosMedicosAsync(int id);
    ValueTask<Especialista> GetByIdWithAvaliacoesAsync(int id);
    ValueTask<Especialista> GetByIdWithLocaisAtendimentoAsync(int id);
    //ValueTask<Especialista> GetByIdWithTagsAsync(int id);
    ValueTask<Especialista> GetByIdWithRespostasAsync(int id);
    ValueTask<PagedResult<Especialista>> GetAllPagedAsync(int pageNumber = 1, int pageSize = 10);
    ValueTask<PagedResult<Especialista>> GetAllByNamePagedAsync(string name, int pageNumber = 1, int pageSize = 10);

    ValueTask<IReadOnlyList<Especialista>> GetAllByEspecialidadeIdAsync(int especialidadeId);

    ValueTask<IReadOnlyList<Tags>> GetAllMarcacoesEspecialistaByIdAsync(int id);
}