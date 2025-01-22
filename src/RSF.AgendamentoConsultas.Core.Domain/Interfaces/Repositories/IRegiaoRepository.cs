using RSF.AgendamentoConsultas.Core.Domain.Entities;


namespace RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

public interface IRegiaoRepository
{
    ValueTask<IReadOnlyList<Regiao>> GetAllAsync();
    ValueTask<Regiao> GetByIdAsync(int id);
    ValueTask<Regiao> GetByIdWithEstadosAsync(int id);
}