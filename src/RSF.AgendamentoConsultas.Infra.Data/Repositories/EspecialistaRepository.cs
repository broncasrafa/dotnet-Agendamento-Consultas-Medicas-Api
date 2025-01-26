using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Infra.Data.Context;
using RSF.AgendamentoConsultas.Infra.Data.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;

namespace RSF.AgendamentoConsultas.Infra.Data.Repositories;

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
                .FirstOrDefaultAsync(c => c.EspecialistaId == id);

    public async ValueTask<Especialista> GetByEmailAsync(string email)
        => await _Context.Especialistas.AsNoTracking()
                .Include(c => c.Especialidades).ThenInclude(e => e.Especialidade).ThenInclude(g => g.EspecialidadeGrupo)
                .Include(c => c.ConveniosMedicosAtendidos).ThenInclude(x => x.ConvenioMedico)
                .Include(c => c.Tags).ThenInclude(t => t.Tag)
                .Include(c => c.LocaisAtendimento)
                .Include(c => c.Avaliacoes).ThenInclude(p => p.Paciente)
                .FirstOrDefaultAsync(c => c.Email == email);
    
    public async ValueTask<Especialista> GetByUserIdAsync(string userId)
        => await _Context.Especialistas.AsNoTracking()
        .Include(a => a.ConsultasAtendidas.Where(st => st.StatusConsultaId == (int)ETipoStatusConsulta.Solicitado || st.StatusConsultaId == (int)ETipoStatusConsulta.Confirmado))
        .FirstOrDefaultAsync(c => c.UserId == userId);

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

    public async ValueTask<Especialista> GetByIdWithRespostasAsync(int id)
        => await _Context.Especialistas.AsNoTracking()
                .Include(c => c.Especialidades).ThenInclude(e => e.Especialidade).ThenInclude(g => g.EspecialidadeGrupo)
                .Include(c => c.Respostas).ThenInclude(x => x.Pergunta).ThenInclude(p => p.Paciente)
                .Include(c => c.Respostas).ThenInclude(x => x.Pergunta).ThenInclude(p => p.Especialidade).ThenInclude(g => g.EspecialidadeGrupo)
                .FirstOrDefaultAsync(c => c.EspecialistaId == id);

    public async ValueTask<IReadOnlyList<Especialista>> GetAllByEspecialidadeIdAsync(int especialidadeId)
        => await _Context.Set<EspecialistaEspecialidade>().AsNoTracking()
                        .Where(c => c.EspecialidadeId == especialidadeId)
                        .Include(e => e.Especialista)
                        .Include(ee => ee.Especialidade).ThenInclude(g => g.EspecialidadeGrupo)
                        .Select(c => c.Especialista)
                        .ToListAsync();

    public async ValueTask<IReadOnlyList<Tags>> GetAllMarcacoesEspecialistaByIdAsync(int id)
    {
        return await _Context.Especialistas
            .AsNoTracking()
            .Where(e => e.EspecialistaId == id) // Filtra o especialista pelo Id
            .SelectMany(e => e.Avaliacoes) // Achata a coleção de Avaliações
                .Where(a => a.TagId.HasValue) // Garantir que o TagId não seja nulo
                .Select(a => a.Marcacao) // Seleciona o objeto Tag associado à avaliação
                .Distinct() // Garante que as Tags sejam distintas
            .ToListAsync();
    }
}