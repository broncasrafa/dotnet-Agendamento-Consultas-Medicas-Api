using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Common;

namespace RSF.AgendamentoConsultas.Core.Domain.Interfaces;

public interface IPerguntaRepository : IBaseRepository<Pergunta>
{
    new ValueTask<Pergunta> GetByIdAsync(int id);
    ValueTask<Pergunta> GetByIdAsync(int perguntaId, int especialidadeId);
}