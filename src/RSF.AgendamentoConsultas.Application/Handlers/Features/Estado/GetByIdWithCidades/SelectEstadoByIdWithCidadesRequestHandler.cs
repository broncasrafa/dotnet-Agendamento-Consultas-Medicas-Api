using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.GetAll;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.GetByIdWithCidades;

public class SelectEstadoByIdWithCidadesRequestHandler : IRequestHandler<SelectEstadoByIdWithCidadesRequest, Result<SelectEstadoResponse>>
{
    private readonly IEstadoRepository _repository;

    public SelectEstadoByIdWithCidadesRequestHandler(IEstadoRepository EstadoRepository) => _repository = EstadoRepository;

    public async Task<Result<SelectEstadoResponse>> Handle(SelectEstadoByIdWithCidadesRequest request, CancellationToken cancellationToken)
    {
        var estados = await _repository.GetByIdWithCidadesAsync(request.Id);
        return await Task.FromResult(SelectEstadoResponse.MapFromEntity(estados));
    }
}