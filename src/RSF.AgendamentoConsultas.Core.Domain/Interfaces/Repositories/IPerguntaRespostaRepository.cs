using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;

namespace RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

public interface IPerguntaRespostaRepository : IBaseRepository<PerguntaResposta>
{
    new ValueTask<PerguntaResposta> GetByIdAsync(int id);
}