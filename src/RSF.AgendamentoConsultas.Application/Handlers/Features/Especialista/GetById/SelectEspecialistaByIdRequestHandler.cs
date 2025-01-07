using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.GetById;

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