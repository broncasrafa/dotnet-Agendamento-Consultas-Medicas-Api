using RSF.AgendamentoConsultas.Domain.Interfaces;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.Response;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.GetByIdWithCidades;

public class SelectEstadoByIdWithCidadesRequestHandler : IRequestHandler<SelectEstadoByIdWithCidadesRequest, Result<EstadoResponse>>
{
    private readonly IEstadoRepository _repository;

    public SelectEstadoByIdWithCidadesRequestHandler(IEstadoRepository EstadoRepository) => _repository = EstadoRepository;

    public async Task<Result<EstadoResponse>> Handle(SelectEstadoByIdWithCidadesRequest request, CancellationToken cancellationToken)
    {
        var estados = await _repository.GetByIdWithCidadesAsync(request.Id);
        return await Task.FromResult(EstadoResponse.MapFromEntity(estados));
    }
}