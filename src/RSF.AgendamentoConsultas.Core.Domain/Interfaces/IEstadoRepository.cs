using RSF.AgendamentoConsultas.Core.Domain.Entities;

namespace RSF.AgendamentoConsultas.Core.Domain.Interfaces;

public interface IEstadoRepository
{
    ValueTask<IReadOnlyList<Estado>> GetAllAsync();
    ValueTask<Estado> GetByIdAsync(int id);
    ValueTask<Estado> GetByIdWithCidadesAsync(int id);
}