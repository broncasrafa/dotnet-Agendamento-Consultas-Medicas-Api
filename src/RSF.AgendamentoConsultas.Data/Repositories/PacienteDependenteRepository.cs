using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Data.Context;
using RSF.AgendamentoConsultas.Data.Repositories.Common;
using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces;

namespace RSF.AgendamentoConsultas.Data.Repositories;

public class PacienteDependenteRepository : BaseRepository<PacienteDependente>, IPacienteDependenteRepository
{
    private readonly AppDbContext _Context;

    public PacienteDependenteRepository(AppDbContext context) : base(context) => _Context = context;

    public new async ValueTask<PacienteDependente> GetByIdAsync(int dependenteId)
    {
        return await _Context.Set<PacienteDependente>().AsNoTracking()
                .Include(p => p.Paciente)
                .Include(p => p.PlanosMedicos)
                .FirstOrDefaultAsync(p => p.DependenteId == dependenteId);
    }
}