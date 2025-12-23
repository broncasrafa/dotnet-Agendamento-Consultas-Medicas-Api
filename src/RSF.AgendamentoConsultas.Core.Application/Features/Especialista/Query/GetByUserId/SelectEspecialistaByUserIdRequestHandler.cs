using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByUserId;

public class SelectEspecialistaByUserIdRequestHandler : IRequestHandler<SelectEspecialistaByUserIdRequest, Result<EspecialistaResponse>>
{
    private readonly IEspecialistaRepository _repository;

    public SelectEspecialistaByUserIdRequestHandler(IEspecialistaRepository repository) => _repository = repository;

    public async Task<Result<EspecialistaResponse>> Handle(SelectEspecialistaByUserIdRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByUserIdAsync(request.UserId);

        NotFoundException.ThrowIfNull(data, $"Especialista com o ID: '{request.UserId}' não encontrado");

        return await Task.FromResult(EspecialistaResponse.MapFromEntity(data));
    }
}