using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;

namespace RSF.AgendamentoConsultas.Domain.Interfaces;

public interface IEspecialidadeRepository : IBaseRepository<Especialidade>
{
    ValueTask<IReadOnlyList<Especialidade>> GetByNameAsync(string name);
    new ValueTask<IReadOnlyList<Especialidade>> GetAllAsync();
    new ValueTask<Especialidade> GetByIdAsync(int id);
}