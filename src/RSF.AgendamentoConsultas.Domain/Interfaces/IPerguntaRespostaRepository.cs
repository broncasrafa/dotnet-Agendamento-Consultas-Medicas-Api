using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;

namespace RSF.AgendamentoConsultas.Domain.Interfaces;

public interface IPerguntaRespostaRepository : IBaseRepository<PerguntaResposta>
{
    new ValueTask<PerguntaResposta> GetByIdAsync(int id);
}