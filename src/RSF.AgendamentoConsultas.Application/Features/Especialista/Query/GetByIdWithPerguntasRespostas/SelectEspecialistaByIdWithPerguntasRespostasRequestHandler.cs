using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Application.Features.Especialista.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Especialista.Query.GetByIdWithPerguntasRespostas;

public class SelectEspecialistaByIdWithPerguntasRespostasRequestHandler : IRequestHandler<SelectEspecialistaByIdWithPerguntasRespostasRequest, Result<EspecialistaResultList<EspecialistaPerguntaResponse>>>
{
    private readonly IEspecialistaRepository _repository;

    public SelectEspecialistaByIdWithPerguntasRespostasRequestHandler(IEspecialistaRepository repository) => _repository = repository;

    public async Task<Result<EspecialistaResultList<EspecialistaPerguntaResponse>>> Handle(SelectEspecialistaByIdWithPerguntasRespostasRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByIdWithRespostasAsync(request.Id);

        NotFoundException.ThrowIfNull(data, $"Especialista com o ID: '{request.Id}' não encontrado");

        //var response = new EspecialistaResultList<EspecialistaPerguntaResponse>(data.EspecialistaId, EspecialistaPerguntaResponse.MapFromEntity(data.Perguntas));

        return Result.Success(new EspecialistaResultList<EspecialistaPerguntaResponse>(data.EspecialistaId, default!));
    }
}