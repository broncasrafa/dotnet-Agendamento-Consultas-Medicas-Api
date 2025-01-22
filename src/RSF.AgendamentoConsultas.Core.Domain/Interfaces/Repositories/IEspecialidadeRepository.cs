using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;

namespace RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

public interface IEspecialidadeRepository : IBaseRepository<Especialidade>
{
    ValueTask<IReadOnlyList<Especialidade>> GetByNameAsync(string name);
    new ValueTask<IReadOnlyList<Especialidade>> GetAllAsync();
    new ValueTask<Especialidade> GetByIdAsync(int id);
}