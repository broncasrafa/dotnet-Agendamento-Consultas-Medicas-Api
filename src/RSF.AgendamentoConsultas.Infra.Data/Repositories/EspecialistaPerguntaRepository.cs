using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Infra.Data.Context;
using RSF.AgendamentoConsultas.Infra.Data.Repositories.Common;

namespace RSF.AgendamentoConsultas.Infra.Data.Repositories;

public class EspecialistaPerguntaRepository : BaseRepository<EspecialistaPergunta>, IEspecialistaPerguntaRepository
{
    private readonly AppDbContext _context;
    public EspecialistaPerguntaRepository(AppDbContext context) : base(context) 
        => _context = context;


}