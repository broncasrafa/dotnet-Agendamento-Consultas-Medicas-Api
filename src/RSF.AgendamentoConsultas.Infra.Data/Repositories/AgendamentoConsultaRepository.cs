﻿using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Infra.Data.Context;
using RSF.AgendamentoConsultas.Infra.Data.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Infra.Data.Repositories;

public class AgendamentoConsultaRepository : BaseRepository<AgendamentoConsulta>, IAgendamentoConsultaRepository
{
    private readonly AppDbContext _Context;

    public AgendamentoConsultaRepository(AppDbContext context) : base(context) => _Context = context;


    public async ValueTask<IReadOnlyList<AgendamentoConsulta>> GetAllByPacienteIdAsync(int pacienteId)
        => await _Context.Agendamentos
            .AsNoTracking()
            .Where(c => c.PacienteId == pacienteId)
            .Include(e => e.Especialista)
            .Include(ee => ee.Especialidade).ThenInclude(g => g.EspecialidadeGrupo)
            .Include(el => el.LocalAtendimento)
            .Include(sc => sc.StatusConsulta)
            .Include(tpc => tpc.TipoConsulta)
            .Include(tpa => tpa.TipoAgendamento)
            .Include(p => p.Paciente)
            .Include(ppm => ppm.PlanoMedico).ThenInclude(cv => cv.ConvenioMedico)
            .Include(d => d.Dependente)
            .Include(dpm => dpm.PlanoMedicoDependente).ThenInclude(dpmcv => dpmcv.ConvenioMedico)
        .ToListAsync();

    new public async ValueTask<AgendamentoConsulta> GetByIdAsync(int agendamentoId)
        => await _Context.Agendamentos
            .AsNoTracking()
            .Include(e => e.Especialista).ThenInclude(ee => ee.Especialidades).ThenInclude(ee => ee.Especialidade)
            .Include(ee => ee.Especialidade).ThenInclude(g => g.EspecialidadeGrupo)
            .Include(el => el.LocalAtendimento)
            .Include(sc => sc.StatusConsulta)
            .Include(tpc => tpc.TipoConsulta)
            .Include(tpa => tpa.TipoAgendamento)
            .Include(p => p.Paciente)
            .Include(ppm => ppm.PlanoMedico).ThenInclude(cv => cv.ConvenioMedico)
            .Include(d => d.Dependente)
            .Include(dpm => dpm.PlanoMedicoDependente).ThenInclude(dpmcv => dpmcv.ConvenioMedico)
        .FirstOrDefaultAsync(c => c.AgendamentoConsultaId == agendamentoId);

    public async ValueTask<AgendamentoConsulta> GetByIdAsync(int agendamentoId, int pacienteId)
        => await _Context.Agendamentos
            .AsNoTracking()
            .Include(e => e.Especialista)
            .Include(ee => ee.Especialidade).ThenInclude(g => g.EspecialidadeGrupo)
            .Include(el => el.LocalAtendimento)
            .Include(p => p.Paciente)
            .Include(d => d.Dependente)
            .FirstOrDefaultAsync(c => c.AgendamentoConsultaId == agendamentoId && c.PacienteId == pacienteId);

    public async ValueTask<AgendamentoConsulta> GetByIdAsync(int agendamentoId, int pacienteId, int dependenteId)
        => await _Context.Agendamentos
            .AsNoTracking()
            .Include(e => e.Especialista)
            .Include(ee => ee.Especialidade).ThenInclude(g => g.EspecialidadeGrupo)
            .Include(el => el.LocalAtendimento)
            .Include(p => p.Paciente)
            .Include(d => d.Dependente)
            .FirstOrDefaultAsync(c => c.AgendamentoConsultaId == agendamentoId && c.PacienteId == pacienteId && c.DependenteId == dependenteId);

    public async ValueTask<IReadOnlyList<AgendamentoConsulta>> GetAllExpiredByPacienteAsync()
    {
        return await _Context.Agendamentos
            .AsNoTracking()
            .Include(p => p.Paciente)
            .Include(e => e.Especialista)
            .Include(ee => ee.Especialidade).ThenInclude(g => g.EspecialidadeGrupo)
            .Include(el => el.LocalAtendimento)
            .Where(c => 
                c.ConfirmedByEspecialistaAt != null && 
                c.StatusConsultaId == (int)ETipoStatusConsulta.Solicitado &&
                c.ConfirmedByEspecialistaAt.Value.AddDays(1) > DateTime.Now
            )
            .ToListAsync();
    }

    public async ValueTask<IReadOnlyList<AgendamentoConsulta>> GetAllExpiredByEspecialistaAsync()
    {
        return await _Context.Agendamentos
            .AsNoTracking()
            .Include(p => p.Paciente)
            .Include(c => c.Especialista)
            .Include(ee => ee.Especialidade).ThenInclude(g => g.EspecialidadeGrupo)
            .Include(el => el.LocalAtendimento)
            .Where(c =>
                c.StatusConsultaId == (int)ETipoStatusConsulta.Solicitado &&
                c.DataConsulta < DateTime.Now
            )
            .ToListAsync();
    }
}