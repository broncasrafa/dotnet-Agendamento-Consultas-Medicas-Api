using RSF.AgendamentoConsultas.Domain.Interfaces;
using MediatR;
using OperationResult;


namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.GetAll;

public class SelectEstadoRequestHandler : IRequestHandler<SelectEstadoRequest, Result<IReadOnlyList<SelectEstadoResponse>>>
{
    private readonly IEstadoRepository _repository;

    public SelectEstadoRequestHandler(IEstadoRepository EstadoRepository) => _repository = EstadoRepository;

    public async Task<Result<IReadOnlyList<SelectEstadoResponse>>> Handle(SelectEstadoRequest request, CancellationToken cancellationToken)
    {
        var estados = await _repository.GetAllAsync();
        return Result.Success<IReadOnlyList<SelectEstadoResponse>>(SelectEstadoResponse.MapFromEntity(estados));
    }
}