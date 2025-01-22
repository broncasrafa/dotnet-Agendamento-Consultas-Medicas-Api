using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetEspecialistaRespostas;

public class SelectEspecialistaRespostasRequestHandler : IRequestHandler<SelectEspecialistaRespostasRequest, Result<EspecialistaResultList<EspecialistaPerguntaRespostaResponse>>>
{
    private readonly IEspecialistaRepository _repository;

    public SelectEspecialistaRespostasRequestHandler(IEspecialistaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<EspecialistaResultList<EspecialistaPerguntaRespostaResponse>>> Handle(SelectEspecialistaRespostasRequest request, CancellationToken cancellationToken)
    {
        var especialista = await _repository.GetByIdWithRespostasAsync(request.EspecialistaId);
        
        NotFoundException.ThrowIfNull(especialista, $"Especialista com o ID: '{request.EspecialistaId}' não encontrado");

        var response = new EspecialistaResultList<EspecialistaPerguntaRespostaResponse>(request.EspecialistaId, EspecialistaPerguntaRespostaResponse.MapFromEntity(especialista.Respostas));

        return Result.Success(response);
    }
}