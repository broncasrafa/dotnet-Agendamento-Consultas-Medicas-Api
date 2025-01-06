using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.GetAll;

public class SelectEstadoRequestHandler : IRequestHandler<SelectEstadoRequest, Result<IReadOnlyList<EstadoResponse>>>
{
    private readonly IEstadoRepository _repository;

    public SelectEstadoRequestHandler(IEstadoRepository EstadoRepository) => _repository = EstadoRepository;

    public async Task<Result<IReadOnlyList<EstadoResponse>>> Handle(SelectEstadoRequest request, CancellationToken cancellationToken)
    {
        var estados = await _repository.GetAllAsync();
        return Result.Success<IReadOnlyList<EstadoResponse>>(EstadoResponse.MapFromEntity(estados));
    }
}