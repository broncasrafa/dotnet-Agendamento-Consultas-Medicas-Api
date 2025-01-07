using System.Drawing.Printing;

using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Data.Context;
using RSF.AgendamentoConsultas.Data.Repositories.Common;
using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Results;

namespace RSF.AgendamentoConsultas.Data.Repositories;

public class EspecialistaRepository : BaseRepository<Especialista>, IEspecialistaRepository
{
    private readonly AppDbContext _Context;

    public EspecialistaRepository(AppDbContext context) : base(context) => _Context = context;



    private static async ValueTask<PagedResult<Especialista>> BindQueryPagedAsync(IQueryable<Especialista> query, int pageNumber, int pageSize)
    {
        var totalCount = await query.CountAsync();

        var paginatedData = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
        .ToListAsync();

        return new PagedResult<Especialista>(data: paginatedData, totalCount: totalCount, pageSize, pageNumber);
    }


    public async ValueTask<PagedResult<Especialista>> GetAllPagedAsync(int pageNumber = 1, int pageSize = 10)
    {
        var query = _Context.Especialistas.AsQueryable();

        return await BindQueryPagedAsync(query, pageNumber, pageSize);
    }

    public async ValueTask<PagedResult<Especialista>> GetAllByNamePagedAsync(string name, int pageNumber = 1, int pageSize = 10)
    {
        var query = _Context.Especialistas.Where(c => c.Nome.Contains(name, StringComparison.InvariantCultureIgnoreCase)).AsQueryable();
        return await BindQueryPagedAsync(query, pageNumber, pageSize);
    }

    public new async ValueTask<Especialista> GetByIdAsync(int id)
        => await _Context.Especialistas.AsNoTracking()
                .Include(c => c.Especialidades).ThenInclude(e => e.Especialidade).ThenInclude(g => g.EspecialidadeGrupo)
                .Include(c => c.ConveniosMedicosAtendidos).ThenInclude(x => x.ConvenioMedico)
                .Include(c => c.Tags).ThenInclude(t => t.Tag)
                .Include(c => c.LocaisAtendimento)
                .Include(c => c.Avaliacoes).ThenInclude(p => p.Paciente)
                .Include(c => c.Perguntas).ThenInclude(p => p.Paciente)
                .Include(c => c.Perguntas).ThenInclude(r => r.Respostas)
                .FirstOrDefaultAsync(c => c.EspecialistaId == id);

    public async ValueTask<Especialista> GetByIdWithEspecialidadesAsync(int id)
        => await _Context.Especialistas.AsNoTracking()
                .Include(c => c.Especialidades).ThenInclude(e => e.Especialidade).ThenInclude(g => g.EspecialidadeGrupo)
                .FirstOrDefaultAsync(c => c.EspecialistaId == id);

    public async ValueTask<Especialista> GetByIdWithConveniosMedicosAsync(int id)
        => await _Context.Especialistas.AsNoTracking()
                .Include(c => c.ConveniosMedicosAtendidos).ThenInclude(x => x.ConvenioMedico)
                .FirstOrDefaultAsync(c => c.EspecialistaId == id);

    public async ValueTask<Especialista> GetByIdWithAvaliacoesAsync(int id)
        => await _Context.Especialistas.AsNoTracking()
                .Include(c => c.Avaliacoes).ThenInclude(p => p.Paciente)
                .FirstOrDefaultAsync(c => c.EspecialistaId == id);

    public async ValueTask<Especialista> GetByIdWithLocaisAtendimentoAsync(int id)
        => await _Context.Especialistas.AsNoTracking()
                .Include(c => c.LocaisAtendimento)
                .FirstOrDefaultAsync(c => c.EspecialistaId == id);

    public async ValueTask<Especialista> GetByIdWithTagsAsync(int id)
        => await _Context.Especialistas.AsNoTracking()
                .Include(c => c.Tags).ThenInclude(t => t.Tag)
                .FirstOrDefaultAsync(c => c.EspecialistaId == id);

    public async ValueTask<Especialista> GetByIdWithPerguntasRespostasAsync(int id)
        => await _Context.Especialistas.AsNoTracking()
                .Include(c => c.Perguntas).ThenInclude(p => p.Paciente)
                .Include(c => c.Perguntas).ThenInclude(r => r.Respostas)
                .FirstOrDefaultAsync(c => c.EspecialistaId == id);
}