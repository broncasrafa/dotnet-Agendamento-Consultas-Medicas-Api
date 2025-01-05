using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Data.Context;
using RSF.AgendamentoConsultas.Data.Repositories.Common;
using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces;

namespace RSF.AgendamentoConsultas.Data.Repositories;

public class ConvenioMedicoRepository : BaseRepository<ConvenioMedico>, IConvenioMedicoRepository
{
    private readonly AppDbContext _Contex;

    public ConvenioMedicoRepository(AppDbContext context) : base(context) => _Contex = context;



    public async ValueTask<ConvenioMedico> GetByIdWithCidadesAtendidasAsync(int id)
    {
        //return await _Contex.ConveniosMedicos
        //    .AsNoTracking()
        //    .Include(c => c.CidadesAtendidas).ThenInclude(e => e.Estado)
        //    .FirstOrDefaultAsync(c => c.ConvenioMedicoId == id);

        return await _Contex.ConveniosMedicos
            .AsNoTracking()
            .Where(c => c.ConvenioMedicoId == id)
            .Select(c => new ConvenioMedico(c.ConvenioMedicoId, c.Nome)
            {
                CidadesAtendidas = c.CidadesAtendidas.Select(cidade => new Cidade
                {
                    CidadeId = cidade.CidadeId,
                    Descricao = cidade.Descricao,
                    EstadoId = cidade.EstadoId,
                    Estado = new Estado
                    {
                        EstadoId = cidade.Estado.EstadoId,
                        Descricao = cidade.Estado.Descricao,
                        Sigla = cidade.Estado.Sigla
                    }
                }).ToList()
            }).FirstOrDefaultAsync();
    }

    public async ValueTask<ConvenioMedico> GetByIdWithCidadesAtendidasAsync(int id, int estadoId)
    {
        return await _Contex.ConveniosMedicos
            .AsNoTracking()
            .Where(c => c.ConvenioMedicoId == id)
            .Select(c => new ConvenioMedico(c.ConvenioMedicoId, c.Nome)
            {
                CidadesAtendidas = c.CidadesAtendidas.Where(cidade => cidade.EstadoId == estadoId)
                    .Select(cidade => new Cidade
                    {
                        CidadeId = cidade.CidadeId,
                        Descricao = cidade.Descricao,
                        EstadoId = cidade.EstadoId,
                        Estado = new Estado
                        {
                            EstadoId = cidade.Estado.EstadoId,
                            Descricao = cidade.Estado.Descricao,
                            Sigla = cidade.Estado.Sigla
                        }
                    }).ToList()
            }).FirstOrDefaultAsync();
    }
}