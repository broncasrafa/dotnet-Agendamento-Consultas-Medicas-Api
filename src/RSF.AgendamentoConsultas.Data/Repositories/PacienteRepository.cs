using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Data.Context;
using RSF.AgendamentoConsultas.Data.Repositories.Common;
using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces;

namespace RSF.AgendamentoConsultas.Data.Repositories;

public class PacienteRepository : BaseRepository<Paciente>, IPacienteRepository
{
    private readonly AppDbContext _Context;
    
    public PacienteRepository(AppDbContext context) : base(context) => _Context = context;


    public async ValueTask<IReadOnlyList<PacientePlanoMedico>> GetByIdWithPlanosMedicosAsync(int pacienteId)
    {
        var paciente = await _Context.Pacientes
                                .AsNoTracking()
                                .Include(p => p.PlanosMedicos)
                                .FirstOrDefaultAsync(p => p.PacienteId == pacienteId);

        return paciente?.PlanosMedicos.ToList() ?? [];
    }
}