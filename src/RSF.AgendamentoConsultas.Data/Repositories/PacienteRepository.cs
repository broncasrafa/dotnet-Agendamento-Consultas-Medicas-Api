using RSF.AgendamentoConsultas.Data.Context;
using RSF.AgendamentoConsultas.Data.Repositories.Common;
using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces;

namespace RSF.AgendamentoConsultas.Data.Repositories;

public class PacienteRepository : BaseRepository<Paciente>, IPacienteRepository
{
    private readonly AppDbContext _Context;
    
    public PacienteRepository(AppDbContext context) : base(context) => _Context = context;
    
}