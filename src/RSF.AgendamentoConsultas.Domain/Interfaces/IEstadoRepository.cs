using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Domain.Interfaces;

public interface IEstadoRepository
{
    ValueTask<IReadOnlyList<Estado>> GetAllAsync();
    ValueTask<Estado> GetByIdAsync(Guid id);
}