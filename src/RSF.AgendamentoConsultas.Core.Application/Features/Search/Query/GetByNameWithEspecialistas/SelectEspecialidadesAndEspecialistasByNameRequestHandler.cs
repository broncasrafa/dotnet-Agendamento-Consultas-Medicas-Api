using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Application.Features.Search.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialidade.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using MediatR;
using OperationResult;


namespace RSF.AgendamentoConsultas.Core.Application.Features.Search.Query.GetByNameWithEspecialistas;

public class SelectEspecialidadesAndEspecialistasByNameRequestHandler : IRequestHandler<SelectEspecialidadesAndEspecialistasByNameRequest, Result<SearchEspecialidadesAndEspecialistasByNameResponse>>
{
    private readonly IEspecialidadeRepository _especialidadeRepository;
    private readonly IEspecialistaRepository _especialistaRepository;

    public SelectEspecialidadesAndEspecialistasByNameRequestHandler(IEspecialidadeRepository especialidadeRepository, IEspecialistaRepository especialistaRepository)
    {
        _especialidadeRepository = especialidadeRepository;
        _especialistaRepository = especialistaRepository;
    }

    public async Task<Result<SearchEspecialidadesAndEspecialistasByNameResponse>> Handle(SelectEspecialidadesAndEspecialistasByNameRequest request, CancellationToken cancellationToken)
    {
        var especialistas = await _especialistaRepository.GetByNameAsync(request.Term);
        var especialidades = await _especialidadeRepository.GetByNameAsync(request.Term);
        var response = new SearchEspecialidadesAndEspecialistasByNameResponse
        (
            EspecialidadeResponse.MapFromEntity(especialidades),
            EspecialistaResponse.MapFromEntity(especialistas)
        );

        return Result.Success(response);
    }
}