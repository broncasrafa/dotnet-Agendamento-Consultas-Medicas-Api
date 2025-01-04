using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Domain.Interfaces;

public interface ICidadeRepository
{
    ValueTask<Cidade> GetByIdAsync(int id);
}