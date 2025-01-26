using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Infra.Data.Context;
using RSF.AgendamentoConsultas.Infra.Data.Repositories.Common;

namespace RSF.AgendamentoConsultas.Infra.Data.Repositories;

public class PacienteDependentePlanoMedicoRepository : BaseRepository<PacienteDependentePlanoMedico>, IPacienteDependentePlanoMedicoRepository
{
    public PacienteDependentePlanoMedicoRepository(AppDbContext context) : base(context)
    {
    }
}