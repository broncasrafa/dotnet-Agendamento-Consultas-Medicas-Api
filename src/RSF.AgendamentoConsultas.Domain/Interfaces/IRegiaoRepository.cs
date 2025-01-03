using RSF.AgendamentoConsultas.Domain.Entities;


namespace RSF.AgendamentoConsultas.Domain.Interfaces;

public interface IRegiaoRepository
{
    ValueTask<IReadOnlyList<Regiao>> GetAllAsync();
    ValueTask<Regiao> GetByIdAsync(int id);
    ValueTask<Regiao> GetByIdWithEstadosAsync(int id);
}