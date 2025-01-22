using RSF.AgendamentoConsultas.Core.Domain.Interfaces;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithAvaliacoes;

public class SelectEspecialistaByIdWithAvaliacoesRequestHandler : IRequestHandler<SelectEspecialistaByIdWithAvaliacoesRequest, Result<EspecialistaResultList<EspecialistaAvaliacaoResponse>>>
{
    private readonly IEspecialistaRepository _repository;

    public SelectEspecialistaByIdWithAvaliacoesRequestHandler(IEspecialistaRepository repository) => _repository = repository;

    public async Task<Result<EspecialistaResultList<EspecialistaAvaliacaoResponse>>> Handle(SelectEspecialistaByIdWithAvaliacoesRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByIdWithAvaliacoesAsync(request.Id);

        NotFoundException.ThrowIfNull(data, $"Especialista com o ID: '{request.Id}' não encontrado");

        var response = new EspecialistaResultList<EspecialistaAvaliacaoResponse>(data.EspecialistaId, EspecialistaAvaliacaoResponse.MapFromEntity(data.Avaliacoes));

        return Result.Success(response);
    }
}