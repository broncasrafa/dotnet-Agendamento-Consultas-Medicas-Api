using RSF.AgendamentoConsultas.Core.Domain.Interfaces;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetById;

public class SelectEspecialistaByIdRequestHandler : IRequestHandler<SelectEspecialistaByIdRequest, Result<EspecialistaResponse>>
{
    private readonly IEspecialistaRepository _repository;

    public SelectEspecialistaByIdRequestHandler(IEspecialistaRepository repository) => _repository = repository;

    public async Task<Result<EspecialistaResponse>> Handle(SelectEspecialistaByIdRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByIdAsync(request.Id);

        NotFoundException.ThrowIfNull(data, $"Especialista com o ID: '{request.Id}' não encontrado");

        return await Task.FromResult(EspecialistaResponse.MapFromEntity(data));
    }
}