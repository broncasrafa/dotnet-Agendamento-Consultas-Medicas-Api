using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Data.Context;
using RSF.AgendamentoConsultas.Data.Repositories.Common;
using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces;

namespace RSF.AgendamentoConsultas.Data.Repositories;

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
}