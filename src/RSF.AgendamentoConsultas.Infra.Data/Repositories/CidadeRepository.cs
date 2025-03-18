using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Infra.Data.Context;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Infra.Data.Repositories;

public class CidadeRepository(AppDbContext context) : ICidadeRepository
{
    private readonly AppDbContext _context = context;
    
    public async ValueTask<Cidade> GetByIdAsync(int id) 
        => await _context.Cidades
                    .AsNoTracking()
                    .Include(e => e.Estado)
                    .Select(c => new Cidade
                    {
                        CidadeId = c.CidadeId,
                        Descricao = c.Descricao,
                        EstadoId = c.EstadoId,
                        Estado = new Estado
                        {
                            EstadoId = c.Estado.EstadoId,
                            Descricao = c.Estado.Descricao,
                            Sigla = c.Estado.Sigla
                        }
                    })
                    .FirstOrDefaultAsync(e => e.CidadeId == id);

    public async ValueTask<IReadOnlyList<Cidade>> GetAllByNameAsync(string name)
    {
        return await _context.Cidades
        .AsNoTracking()
        .Include(e => e.Estado).ThenInclude(r => r.Regiao)
        .Where(c => EF.Functions.Collate(c.Descricao, "Latin1_General_CI_AS").Contains(name))
        .ToListAsync();
    }

    public async ValueTask<IReadOnlyList<Cidade>> GetAllAsync()
    {
        return await _context.Cidades.AsNoTracking()
                        .Include(e => e.Estado).ThenInclude(r => r.Regiao)
                        .ToListAsync();
    }
}