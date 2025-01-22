using RSF.AgendamentoConsultas.Core.Domain.Interfaces;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;


namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithLocaisAtendimento;

public class SelectEspecialistaByIdWithLocaisAtendimentoRequestHandler : IRequestHandler<SelectEspecialistaByIdWithLocaisAtendimentoRequest, Result<EspecialistaResultList<EspecialistaLocalAtendimentoResponse>>>
{
    private readonly IEspecialistaRepository _repository;

    public SelectEspecialistaByIdWithLocaisAtendimentoRequestHandler(IEspecialistaRepository repository) => _repository = repository;

    public async Task<Result<EspecialistaResultList<EspecialistaLocalAtendimentoResponse>>> Handle(SelectEspecialistaByIdWithLocaisAtendimentoRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByIdWithLocaisAtendimentoAsync(request.Id);

        NotFoundException.ThrowIfNull(data, $"Especialista com o ID: '{request.Id}' não encontrado");

        var response = new EspecialistaResultList<EspecialistaLocalAtendimentoResponse>(data.EspecialistaId, EspecialistaLocalAtendimentoResponse.MapFromEntity(data.LocaisAtendimento));

        return Result.Success(response);
    }
}