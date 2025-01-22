using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Common;

namespace RSF.AgendamentoConsultas.Core.Domain.Interfaces;

public interface IPerguntaRespostaRepository : IBaseRepository<PerguntaResposta>
{
    new ValueTask<PerguntaResposta> GetByIdAsync(int id);
}