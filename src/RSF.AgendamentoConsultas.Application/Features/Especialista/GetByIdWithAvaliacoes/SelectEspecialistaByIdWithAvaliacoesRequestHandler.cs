using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Features.Especialista.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Especialista.GetByIdWithAvaliacoes;

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