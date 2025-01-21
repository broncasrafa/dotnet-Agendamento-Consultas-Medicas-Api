using RSF.AgendamentoConsultas.Data.Context;
using RSF.AgendamentoConsultas.Data.Repositories.Common;
using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces;

namespace RSF.AgendamentoConsultas.Data.Repositories;

public class TipoStatusConsultaRepository : BaseRepository<TipoStatusConsulta>, ITipoStatusConsultaRepository
{
    public TipoStatusConsultaRepository(AppDbContext context) : base(context)
    {
    }
}