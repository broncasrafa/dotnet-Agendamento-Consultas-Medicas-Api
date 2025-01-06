using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Data.Context;
using RSF.AgendamentoConsultas.Data.Repositories.Common;
using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces;

namespace RSF.AgendamentoConsultas.Data.Repositories;

public class EspecialidadeRepository : BaseRepository<Especialidade>, IEspecialidadeRepository
{
    private readonly AppDbContext _Context;

    public EspecialidadeRepository(AppDbContext context) : base(context) => _Context = context;

    public async ValueTask<IReadOnlyList<Especialidade>> GetByNameAsync(string name)
        => await _Context.Especialidades
                    .AsNoTracking()
                    .Include(g => g.EspecialidadeGrupo)
                    .Where(c => c.Nome.Contains(name))
                    .ToListAsync();

    public new async ValueTask<IReadOnlyList<Especialidade>> GetAllAsync()
        => await _Context.Especialidades
                    .AsNoTracking()
                    .Include(g => g.EspecialidadeGrupo)
                    .ToListAsync();

    public new async ValueTask<Especialidade> GetByIdAsync(int id)
        => await _Context.Especialidades
                    .AsNoTracking()
                    .Include(g => g.EspecialidadeGrupo)
                    .FirstOrDefaultAsync(c => c.EspecialidadeId == id);
}