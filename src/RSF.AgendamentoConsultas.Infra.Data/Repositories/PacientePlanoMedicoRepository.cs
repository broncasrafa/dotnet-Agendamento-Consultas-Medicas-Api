﻿using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Infra.Data.Context;
using RSF.AgendamentoConsultas.Infra.Data.Repositories.Common;

namespace RSF.AgendamentoConsultas.Infra.Data.Repositories;

public class PacientePlanoMedicoRepository : BaseRepository<PacientePlanoMedico>, IPacientePlanoMedicoRepository
{
    public PacientePlanoMedicoRepository(AppDbContext context) : base(context)
    {
    }
}