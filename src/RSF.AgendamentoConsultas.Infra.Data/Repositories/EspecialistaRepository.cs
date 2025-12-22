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


    public async ValueTask<PagedResult<Especialista>> GetAllPagedAsync(int pageNumber = 1, int pageSize = 10)
    {
        var query = _Context.Especialistas
                        .Include(c => c.Especialidades).ThenInclude(e => e.Especialidade).ThenInclude(g => g.EspecialidadeGrupo)
                        .Include(c => c.ConveniosMedicosAtendidos).ThenInclude(x => x.ConvenioMedico)
                        .Include(c => c.LocaisAtendimento)
                        .Include(c => c.Avaliacoes).ThenInclude(p => p.Paciente)
                        .Include(c => c.Avaliacoes).ThenInclude(t => t.Marcacao)
                        .Include(c => c.PerguntasEspecialista)
                    .AsQueryable();

        return await BindQueryPagedAsync(query, pageNumber, pageSize, orderBy: c => c.EspecialistaId);
    }

    public async ValueTask<PagedResult<Especialista>> GetAllByNamePagedAsync(string name, int pageNumber = 1, int pageSize = 10)
    {
        var query = _Context.Especialistas
                        .Include(c => c.Especialidades).ThenInclude(e => e.Especialidade).ThenInclude(g => g.EspecialidadeGrupo)
                        .Include(c => c.ConveniosMedicosAtendidos).ThenInclude(x => x.ConvenioMedico)
                        .Include(c => c.LocaisAtendimento)
                        .Include(c => c.Avaliacoes).ThenInclude(p => p.Paciente)
                        .Include(c => c.Avaliacoes).ThenInclude(t => t.Marcacao)
                        .Include(c => c.PerguntasEspecialista)
                        .Where(c => EF.Functions.Collate(c.Nome, "Latin1_General_CI_AS").Contains(name))
                        .AsQueryable();
        return await BindQueryPagedAsync(query, pageNumber, pageSize, orderBy: c => c.EspecialistaId);
    }

    public async ValueTask<PagedResult<Especialista>> GetAllByFiltersPagedAsync(int? especialidadeId, string cidade, int pageNumber = 1, int pageSize = 10)
    {
        var query = _Context.Especialistas.AsNoTracking()
                .Include(c => c.Especialidades).ThenInclude(e => e.Especialidade).ThenInclude(g => g.EspecialidadeGrupo)
                .Include(c => c.ConveniosMedicosAtendidos).ThenInclude(x => x.ConvenioMedico)
                .Include(c => c.LocaisAtendimento)
                .Include(c => c.Avaliacoes).ThenInclude(p => p.Paciente)
                .Include(c => c.Avaliacoes).ThenInclude(t => t.Marcacao)
                .AsQueryable();

        if (especialidadeId.HasValue)
            query = query.Where(e => e.Especialidades.Any(es => es.EspecialidadeId == especialidadeId.Value));

        if (!string.IsNullOrWhiteSpace(cidade))
        {
            query = query.Where(e => e.LocaisAtendimento.Any(l => EF.Functions.Collate(l.Cidade, "SQL_Latin1_General_CP1_CI_AI").Contains(EF.Functions.Collate(cidade, "SQL_Latin1_General_CP1_CI_AI"))));
        }

        // 🔹 Paginação otimizada para evitar timeout
        //query = query.OrderBy(e => e.EspecialistaId); // Ordem por ID melhora performance do banco

        return await BindQueryPagedAsync(query, pageNumber, pageSize, orderBy: c => c.EspecialistaId);
    }

    public async ValueTask<PagedResult<Especialista>> GetAllByEspecialidadeTermPagedAsync(string especialidadeTerm, int pageNumber = 1, int pageSize = 10)
    {
        var query = _Context.Especialistas.AsNoTracking()
                .Include(c => c.Especialidades).ThenInclude(e => e.Especialidade).ThenInclude(g => g.EspecialidadeGrupo)
                .Include(c => c.ConveniosMedicosAtendidos).ThenInclude(x => x.ConvenioMedico)
                .Include(c => c.LocaisAtendimento)
                .Include(c => c.Avaliacoes).ThenInclude(p => p.Paciente)
                .Include(c => c.Avaliacoes).ThenInclude(t => t.Marcacao)
                .Where(es => es.Especialidades.Any(ee => EF.Functions.Collate(ee.Especialidade.Term, "SQL_Latin1_General_CP1_CI_AI") == especialidadeTerm))
                .AsQueryable();

        return await BindQueryPagedAsync(query, pageNumber, pageSize, orderBy: c => c.EspecialistaId);
    }

    public async ValueTask<IReadOnlyList<Especialista>> GetByNameAsync(string name)
        => await _Context.Especialistas
                    .AsNoTracking()
                    .Where(c => EF.Functions.Collate(c.Nome, "Latin1_General_CI_AS").Contains(name))
                    .ToListAsync();

    public new async ValueTask<Especialista> GetByIdAsync(int id)
        => await _Context.Especialistas.AsNoTracking()
                .Include(c => c.Especialidades).ThenInclude(e => e.Especialidade).ThenInclude(g => g.EspecialidadeGrupo)
                .Include(c => c.ConveniosMedicosAtendidos).ThenInclude(x => x.ConvenioMedico)
                .Include(c => c.LocaisAtendimento)
                .Include(c => c.Avaliacoes).ThenInclude(p => p.Paciente)
                .Include(c => c.Avaliacoes).ThenInclude(t => t.Marcacao)
                .Include(c => c.PerguntasEspecialista)
                .FirstOrDefaultAsync(c => c.EspecialistaId == id);

    public async ValueTask<Especialista> GetByEmailAsync(string email)
        => await _Context.Especialistas.AsNoTracking()
                .Include(c => c.Especialidades).ThenInclude(e => e.Especialidade).ThenInclude(g => g.EspecialidadeGrupo)
                .Include(c => c.ConveniosMedicosAtendidos).ThenInclude(x => x.ConvenioMedico)
                .Include(c => c.LocaisAtendimento)
                .Include(c => c.Avaliacoes).ThenInclude(p => p.Paciente)
                .Include(c => c.Avaliacoes).ThenInclude(t => t.Marcacao)
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
                .Include(c => c.Avaliacoes).ThenInclude(t => t.Marcacao)
                .FirstOrDefaultAsync(c => c.EspecialistaId == id);

    public async ValueTask<Especialista> GetByIdWithLocaisAtendimentoAsync(int id)
        => await _Context.Especialistas.AsNoTracking()
                .Include(c => c.LocaisAtendimento)
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