using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Infra.Data.Context;
using RSF.AgendamentoConsultas.Infra.Data.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Infra.Data.Repositories;

public class PacienteDependenteRepository : BaseRepository<PacienteDependente>, IPacienteDependenteRepository
{
    private readonly AppDbContext _Context;

    public PacienteDependenteRepository(AppDbContext context) : base(context) => _Context = context;

    new public async ValueTask<PacienteDependente> GetByIdAsync(int dependenteId)
    {
        return await _Context.Set<PacienteDependente>().AsNoTracking()
                .Include(p => p.Paciente)
                .Include(p => p.PlanosMedicos).ThenInclude(cm => cm.ConvenioMedico)
                .FirstOrDefaultAsync(p => p.DependenteId == dependenteId);
    }
}