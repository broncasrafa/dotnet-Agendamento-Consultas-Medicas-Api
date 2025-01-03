using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Data.Context;
using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces;

namespace RSF.AgendamentoConsultas.Data.Repositories;

public class EstadoRepository(AppDbContext context) : IEstadoRepository
{
    private readonly AppDbContext _context = context;

    public async ValueTask<IReadOnlyList<Estado>> GetAllAsync() => await _context.Estados.ToListAsync();

    public async ValueTask<Estado> GetByIdAsync(Guid id) => await _context.Estados.FindAsync(id);
}