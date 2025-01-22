using RSF.AgendamentoConsultas.Infra.Data.Context;
using RSF.AgendamentoConsultas.Infra.Data.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Infra.Data.Repositories;

public class TipoStatusConsultaRepository : BaseRepository<TipoStatusConsulta>, ITipoStatusConsultaRepository
{
    public TipoStatusConsultaRepository(AppDbContext context) : base(context)
    {
    }
}