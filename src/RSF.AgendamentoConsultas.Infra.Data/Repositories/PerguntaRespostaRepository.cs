using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Infra.Data.Context;
using RSF.AgendamentoConsultas.Infra.Data.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces;

namespace RSF.AgendamentoConsultas.Infra.Data.Repositories;

public class PerguntaRespostaRepository : BaseRepository<PerguntaResposta>, IPerguntaRespostaRepository
{
    private readonly AppDbContext _Context;
    public PerguntaRespostaRepository(AppDbContext context) : base(context) => _Context = context;

    new public async ValueTask<PerguntaResposta> GetByIdAsync(int id)
        => await _Context.Respostas
            .AsNoTracking()
            .Include(c => c.Pergunta).ThenInclude(es => es.Especialidade).ThenInclude(g => g.EspecialidadeGrupo)
            .Include(e => e.Especialista).ThenInclude(es => es.Especialidades).ThenInclude(ep => ep.Especialidade)
            .Include(rc => rc.Reacoes).ThenInclude(p => p.Paciente)
            .FirstOrDefaultAsync(c => c.PerguntaRespostaId == id);
}