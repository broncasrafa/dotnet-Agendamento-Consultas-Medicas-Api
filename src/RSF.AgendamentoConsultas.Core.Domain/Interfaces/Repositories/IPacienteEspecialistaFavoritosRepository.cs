using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;

namespace RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

public interface IPacienteEspecialistaFavoritosRepository : IBaseRepository<PacienteEspecialistaFavoritos>
{
    ValueTask<PagedResult<Especialista>> GetAllPagedAsync(int pacienteId, int pageNumber = 1, int pageSize = 10);
    ValueTask<PacienteEspecialistaFavoritos> GetByIdsAsync(int pacienteId, int especialistaId);
}