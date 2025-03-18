using RSF.AgendamentoConsultas.Core.Domain.Entities;

namespace RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

public interface ICidadeRepository
{
    ValueTask<Cidade> GetByIdAsync(int id);
    ValueTask<IReadOnlyList<Cidade>> GetAllAsync();
    ValueTask<IReadOnlyList<Cidade>> GetAllByNameAsync(string name);
}