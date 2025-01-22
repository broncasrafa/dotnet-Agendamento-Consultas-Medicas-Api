using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Infra.Data.Context;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces;

namespace RSF.AgendamentoConsultas.Infra.Data.Repositories;

public class RegiaoRepository(AppDbContext context) : IRegiaoRepository
{
    private readonly AppDbContext _context = context;

    public async ValueTask<IReadOnlyList<Regiao>> GetAllAsync() 
        => await _context.Regioes.ToListAsync();

    public async ValueTask<Regiao> GetByIdAsync(int id) 
        => await _context.Regioes.FindAsync(id);

    public async ValueTask<Regiao> GetByIdWithEstadosAsync(int id)
        => await _context.Regioes.Include(e => e.Estados).FirstOrDefaultAsync(e => e.RegiaoId == id);
}