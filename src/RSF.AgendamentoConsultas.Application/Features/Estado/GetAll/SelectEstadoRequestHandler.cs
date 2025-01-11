using RSF.AgendamentoConsultas.Domain.Interfaces;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Features.Estado.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Estado.GetAll;

public class SelectEstadoRequestHandler : IRequestHandler<SelectEstadoRequest, Result<IReadOnlyList<EstadoResponse>>>
{
    private readonly IEstadoRepository _repository;

    public SelectEstadoRequestHandler(IEstadoRepository EstadoRepository) => _repository = EstadoRepository;

    public async Task<Result<IReadOnlyList<EstadoResponse>>> Handle(SelectEstadoRequest request, CancellationToken cancellationToken)
    {
        var estados = await _repository.GetAllAsync();
        return Result.Success(EstadoResponse.MapFromEntity(estados));
    }
}