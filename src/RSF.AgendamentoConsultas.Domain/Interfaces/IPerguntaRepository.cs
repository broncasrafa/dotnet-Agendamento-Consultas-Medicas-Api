using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;

namespace RSF.AgendamentoConsultas.Domain.Interfaces;

public interface IPerguntaRepository : IBaseRepository<Pergunta>
{
    ValueTask<Pergunta> GetByIdAsync(int perguntaId, int especialidadeId);
}