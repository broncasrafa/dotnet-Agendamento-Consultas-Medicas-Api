using RSF.AgendamentoConsultas.Infra.Data.Context;
using RSF.AgendamentoConsultas.Infra.Data.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Infra.Data.Repositories;

public class PerguntaRespostaReacaoRepository : BaseRepository<PerguntaRespostaReacao>, IPerguntaRespostaReacaoRepository
{
    private readonly AppDbContext _Context;

    public PerguntaRespostaReacaoRepository(AppDbContext context) : base(context) => _Context = context;

    public async Task<int> DeleteReactionPerguntaRespostaAsync(int respostaId, int pacienteId)
    {
        var reacao = await GetByFilterAsync(c => c.RespostaId == respostaId && c.PacienteId == pacienteId);
        var rowsAffected = await RemoveAsync(reacao);
        return rowsAffected;
    }
}