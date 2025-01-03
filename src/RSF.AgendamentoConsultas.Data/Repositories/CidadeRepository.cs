using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Data.Context;
using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces;

namespace RSF.AgendamentoConsultas.Data.Repositories;

public class CidadeRepository(AppDbContext context) : ICidadeRepository
{
    private readonly AppDbContext _context = context;

    public async ValueTask<IReadOnlyList<Cidade>> GetAllAsync() => await _context.Cidades.ToListAsync();

    public async ValueTask<Cidade> GetByIdAsync(Guid id) => await _context.Cidades.FindAsync(id);
}