using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;

namespace RSF.AgendamentoConsultas.Domain.Interfaces;

public interface IEspecialistaRepository : IBaseRepository<Especialista>
{
    ValueTask<Especialista> GetByIdWithEspecialidadesAsync(int id);
    ValueTask<Especialista> GetByIdWithConveniosMedicosAsync(int id);
    ValueTask<Especialista> GetByIdWithAvaliacoesAsync(int id);
    ValueTask<Especialista> GetByIdWithLocaisAtendimentoAsync(int id);
    ValueTask<Especialista> GetByIdWithTagsAsync(int id);
    ValueTask<Especialista> GetByIdWithPerguntasRespostasAsync(int id);
}