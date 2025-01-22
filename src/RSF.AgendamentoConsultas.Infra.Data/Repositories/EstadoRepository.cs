using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Infra.Data.Context;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces;

namespace RSF.AgendamentoConsultas.Infra.Data.Repositories;

public class EstadoRepository(AppDbContext context) : IEstadoRepository
{
    private readonly AppDbContext _context = context;

    public async ValueTask<IReadOnlyList<Estado>> GetAllAsync() 
        => await _context.Estados.ToListAsync();

    public async ValueTask<Estado> GetByIdAsync(int id) 
        => await _context.Estados.FindAsync(id);

    public async ValueTask<Estado> GetByIdWithCidadesAsync(int id) 
        => await _context.Estados
                    .AsNoTracking()
                    .Include(c => c.Cidades)
                    .Select(c => new Estado
                    {
                        EstadoId = c.EstadoId,
                        Descricao = c.Descricao,
                        Sigla = c.Sigla,
                        RegiaoId = c.RegiaoId,
                        Regiao = new Regiao
                        {
                            RegiaoId = c.Regiao.RegiaoId,
                            Descricao = c.Regiao.Descricao
                        },
                        Cidades = c.Cidades.Select(c => new Cidade
                        {
                            CidadeId = c.CidadeId,
                            Descricao = c.Descricao,
                            EstadoId = c.EstadoId
                        }).ToList()
                    })
                    .FirstOrDefaultAsync(e => e.EstadoId == id);
}