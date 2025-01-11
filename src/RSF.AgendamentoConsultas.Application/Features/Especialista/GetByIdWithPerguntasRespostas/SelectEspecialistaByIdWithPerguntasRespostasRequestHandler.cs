using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Features.Especialista.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Especialista.GetByIdWithPerguntasRespostas;

public class SelectEspecialistaByIdWithPerguntasRespostasRequestHandler : IRequestHandler<SelectEspecialistaByIdWithPerguntasRespostasRequest, Result<EspecialistaResultList<EspecialistaPerguntaResponse>>>
{
    private readonly IEspecialistaRepository _repository;

    public SelectEspecialistaByIdWithPerguntasRespostasRequestHandler(IEspecialistaRepository repository) => _repository = repository;

    public async Task<Result<EspecialistaResultList<EspecialistaPerguntaResponse>>> Handle(SelectEspecialistaByIdWithPerguntasRespostasRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByIdWithPerguntasRespostasAsync(request.Id);

        NotFoundException.ThrowIfNull(data, $"Especialista com o ID: '{request.Id}' não encontrado");

        var response = new EspecialistaResultList<EspecialistaPerguntaResponse>(data.EspecialistaId, EspecialistaPerguntaResponse.MapFromEntity(data.Perguntas));

        return Result.Success(response);
    }
}