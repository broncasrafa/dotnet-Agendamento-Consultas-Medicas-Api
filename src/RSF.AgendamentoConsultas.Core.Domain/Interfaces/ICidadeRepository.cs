using RSF.AgendamentoConsultas.Core.Domain.Entities;

namespace RSF.AgendamentoConsultas.Core.Domain.Interfaces;

public interface ICidadeRepository
{
    ValueTask<Cidade> GetByIdAsync(int id);
}