using RSF.AgendamentoConsultas.Domain.Entities;


namespace RSF.AgendamentoConsultas.Domain.Interfaces;

public interface ICidadeRepository
{
    ValueTask<IReadOnlyList<Cidade>> GetAllAsync();
    ValueTask<Cidade> GetByIdAsync(Guid id);
}