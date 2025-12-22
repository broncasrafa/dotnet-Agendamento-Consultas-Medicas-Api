using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Infra.Data.Context;
using RSF.AgendamentoConsultas.Infra.Data.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Infra.Data.Repositories;

public class PacienteEspecialistaFavoritosRepository : BaseRepository<PacienteEspecialistaFavoritos>, IPacienteEspecialistaFavoritosRepository
{
    private readonly AppDbContext _Context;

    public PacienteEspecialistaFavoritosRepository(AppDbContext context) : base(context) => _Context = context;

    public async ValueTask<PagedResult<Especialista>> GetAllPagedAsync(int pacienteId, int pageNumber = 1, int pageSize = 10)
    {
        var query = _Context.Favoritos.AsNoTracking()
            .Include(e => e.Especialista).ThenInclude(e => e.Especialidades).ThenInclude(e => e.Especialidade)
            .Where(c => c.PacienteId == pacienteId)
            .Select(c => c.Especialista)
            .AsQueryable();

        return await BindQueryPagedAsync(query, pageNumber, pageSize, orderBy: c => c.EspecialistaId);
    }

    public async ValueTask<PacienteEspecialistaFavoritos> GetByIdsAsync(int pacienteId, int especialistaId)
    {
        return await _Context.Favoritos.AsNoTracking()
            .FirstOrDefaultAsync(c => c.PacienteId == pacienteId && c.EspecialistaId == especialistaId);
    }
}