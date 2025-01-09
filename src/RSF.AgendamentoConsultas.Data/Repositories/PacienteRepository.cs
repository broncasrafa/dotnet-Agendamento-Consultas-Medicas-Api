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

    public async ValueTask<Paciente> GetByIdDetailsAsync(int pacienteId)
        => await _Context.Pacientes
                    .AsNoTracking()
                    .Include(c => c.Dependentes).ThenInclude(d => d.PlanosMedicos).ThenInclude(cm => cm.ConvenioMedico)
                    .Include(c => c.Dependentes).ThenInclude(p => p.Paciente)
                    .Include(c => c.PlanosMedicos).ThenInclude(cm => cm.ConvenioMedico)
                    .FirstOrDefaultAsync(p => p.PacienteId == pacienteId);


    public async ValueTask<IReadOnlyList<PacientePlanoMedico>> GetPlanosMedicosPacienteByIdAsync(int pacienteId)
    {
        var paciente = await _Context.Pacientes
                                .AsNoTracking()
                                .Include(p => p.PlanosMedicos).ThenInclude(cm => cm.ConvenioMedico)
                                .FirstOrDefaultAsync(p => p.PacienteId == pacienteId);

        return paciente?.PlanosMedicos?.ToList() ?? [];
    }

    public async ValueTask<IReadOnlyList<PacienteDependente>> GetDependentesPacienteByIdAsync(int pacienteId)
    {
        var paciente = await _Context.Pacientes
                                .AsNoTracking()
                                .Include(c => c.Dependentes).ThenInclude(d => d.PlanosMedicos).ThenInclude(cm => cm.ConvenioMedico)
                                .Include(c => c.Dependentes).ThenInclude(p => p.Paciente)
                                .FirstOrDefaultAsync(p => p.PacienteId == pacienteId);

        return paciente?.Dependentes?.ToList() ?? [];
    }
}