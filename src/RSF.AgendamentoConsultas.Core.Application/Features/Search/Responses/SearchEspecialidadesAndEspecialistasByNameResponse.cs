using RSF.AgendamentoConsultas.Core.Application.Features.Especialidade.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Search.Responses;

public class SearchEspecialidadesAndEspecialistasByNameResponse
{
    public IEnumerable<EspecialidadeResponse> Especialidades { get; set; }
    public IEnumerable<EspecialistaResponse> Especialistas { get; set; }

    public SearchEspecialidadesAndEspecialistasByNameResponse(
        IEnumerable<EspecialidadeResponse> especialidades, 
        IEnumerable<EspecialistaResponse> especialistas)
    {
        Especialidades = especialidades;
        Especialistas = especialistas;
    }
}