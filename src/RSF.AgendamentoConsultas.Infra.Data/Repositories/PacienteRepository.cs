using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Infra.Data.Context;
using RSF.AgendamentoConsultas.Infra.Data.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;

namespace RSF.AgendamentoConsultas.Infra.Data.Repositories;

public class PacienteRepository : BaseRepository<Paciente>, IPacienteRepository
{
    private readonly AppDbContext _Context;
    
    public PacienteRepository(AppDbContext context) : base(context) => _Context = context;

    public async ValueTask<Paciente> GetByIdDetailsAsync(int pacienteId)
        => await _Context.Pacientes
                    .AsNoTracking()
                    .Include(c => c.Dependentes).ThenInclude(d => d.PlanosMedicos).ThenInclude(cm => cm.ConvenioMedico)
                    .Include(c => c.PlanosMedicos).ThenInclude(cm => cm.ConvenioMedico)
                    .FirstOrDefaultAsync(p => p.PacienteId == pacienteId);

    public async ValueTask<Paciente> GetByEmailAsync(string email)
        => await _Context.Pacientes
                    .AsNoTracking()
                    .Include(c => c.Dependentes).ThenInclude(d => d.PlanosMedicos).ThenInclude(cm => cm.ConvenioMedico)
                    .Include(c => c.PlanosMedicos).ThenInclude(cm => cm.ConvenioMedico)
                    .FirstOrDefaultAsync(p => p.Email == email);
    
    public async ValueTask<Paciente> GetByUserIdAsync(string userId)
        => await _Context.Pacientes
                    .AsNoTracking()
                    .Include(c => c.Dependentes).ThenInclude(d => d.PlanosMedicos).ThenInclude(cm => cm.ConvenioMedico)
                    .Include(c => c.PlanosMedicos).ThenInclude(cm => cm.ConvenioMedico)
                    .Include(a => a.AgendamentosRealizados.Where(st => st.StatusConsultaId == (int)ETipoStatusConsulta.Solicitado || st.StatusConsultaId == (int)ETipoStatusConsulta.Confirmado))
                    .FirstOrDefaultAsync(p => p.UserId == userId);


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
                                .FirstOrDefaultAsync(p => p.PacienteId == pacienteId);

        return paciente?.Dependentes?.ToList() ?? [];
    }

    public async ValueTask<IReadOnlyList<EspecialistaAvaliacao>> GetAvaliacoesPacienteByIdAsync(int pacienteId)
    {
        var paciente = await _Context.Pacientes
                                .AsNoTracking()
                                .Include(c => c.AvaliacoesFeitas).ThenInclude(e => e.Especialista)
                                .FirstOrDefaultAsync(p => p.PacienteId == pacienteId);

        return paciente?.AvaliacoesFeitas?.ToList() ?? [];
    }
}