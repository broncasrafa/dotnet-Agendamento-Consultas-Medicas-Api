using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Infra.Data.Context;
using RSF.AgendamentoConsultas.Infra.Data.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;

namespace RSF.AgendamentoConsultas.Infra.Data.Repositories;

public class PerguntaRepository : BaseRepository<Pergunta>, IPerguntaRepository
{
    private readonly AppDbContext _Context;
    public PerguntaRepository(AppDbContext context) : base(context) => _Context = context;

    new public async ValueTask<Pergunta> GetByIdAsync(int id)
        => await _Context.Perguntas
        .AsNoTracking()
        .Include(c => c.Paciente)
        .Include(r => r.Respostas).ThenInclude(e => e.Especialista).ThenInclude(c => c.Especialidades).ThenInclude(g => g.Especialidade)
        .Include(r => r.Respostas).ThenInclude(rc => rc.Reacoes).ThenInclude(p => p.Paciente)
        .Include(es => es.Especialidade).ThenInclude(g => g.EspecialidadeGrupo)
        .FirstOrDefaultAsync(c => c.PerguntaId == id);

    public async ValueTask<Pergunta> GetByIdAsync(int perguntaId, int especialidadeId)
        => await _Context.Perguntas
        .AsNoTracking()
        .Include(c => c.Paciente)
        .Include(r => r.Respostas).ThenInclude(e => e.Especialista).ThenInclude(c => c.Especialidades).ThenInclude(g => g.Especialidade)
        .Include(r => r.Respostas).ThenInclude(rc => rc.Reacoes).ThenInclude(p => p.Paciente)
        .Include(es => es.Especialidade).ThenInclude(g => g.EspecialidadeGrupo)
        .FirstOrDefaultAsync(c => c.PerguntaId == perguntaId && c.EspecialidadeId == especialidadeId);

    public async ValueTask<PagedResult<Pergunta>> GetAllPagedAsync(int pageNumber = 1, int pageSize = 10)
    {
        var query = _Context.Perguntas
                        .AsNoTracking()
                        .Include(c => c.Paciente)
                        .Include(r => r.Respostas).ThenInclude(e => e.Especialista).ThenInclude(c => c.Especialidades).ThenInclude(g => g.Especialidade)
                        .Include(r => r.Respostas).ThenInclude(e => e.Especialista).ThenInclude(c => c.Avaliacoes)
                        .Include(r => r.Respostas).ThenInclude(rc => rc.Reacoes).ThenInclude(p => p.Paciente)
                        .Include(es => es.Especialidade).ThenInclude(g => g.EspecialidadeGrupo)
                        .AsQueryable();

        return await BindQueryPagedAsync(query, pageNumber, pageSize);
    }

    public async ValueTask<PagedResult<Pergunta>> GetByListaPerguntaIdsPagedAsync(List<int> perguntaIds, int pageNumber = 1, int pageSize = 10)
    {
        var query = _Context.Perguntas
                        .AsNoTracking()
                        .Include(c => c.Paciente)
                        .Include(r => r.Respostas).ThenInclude(e => e.Especialista).ThenInclude(c => c.Especialidades).ThenInclude(g => g.Especialidade)
                        .Include(r => r.Respostas).ThenInclude(e => e.Especialista).ThenInclude(c => c.Avaliacoes)
                        .Include(r => r.Respostas).ThenInclude(rc => rc.Reacoes).ThenInclude(p => p.Paciente)
                        .Where(c => perguntaIds.Contains(c.PerguntaId))
                        .AsQueryable();

        return await BindQueryPagedAsync(query, pageNumber, pageSize);
    }
}