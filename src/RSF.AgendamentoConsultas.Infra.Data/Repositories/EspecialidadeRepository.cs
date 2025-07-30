using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Infra.Data.Context;
using RSF.AgendamentoConsultas.Infra.Data.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Infra.Data.Repositories;

public class EspecialidadeRepository : BaseRepository<Especialidade>, IEspecialidadeRepository
{
    private readonly AppDbContext _Context;

    public EspecialidadeRepository(AppDbContext context) : base(context) => _Context = context;

    public async ValueTask<IReadOnlyList<Especialidade>> GetByNameAsync(string name)
        => await _Context.Especialidades
                    .AsNoTracking()
                    .Include(g => g.EspecialidadeGrupo)
                    .Where(c => EF.Functions.Collate(c.Nome, "Latin1_General_CI_AS").Contains(name))
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