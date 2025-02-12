using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Estado.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Estado.Query.GetAll;

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