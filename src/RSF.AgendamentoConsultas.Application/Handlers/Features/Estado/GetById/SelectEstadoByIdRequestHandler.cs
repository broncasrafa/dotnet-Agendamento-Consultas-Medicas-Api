using RSF.AgendamentoConsultas.Domain.Interfaces;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.Response;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.GetById;

public class SelectEstadoByIdRequestHandler : IRequestHandler<SelectEstadoByIdRequest, Result<EstadoResponse>>
{
    private readonly IEstadoRepository _repository;

    public SelectEstadoByIdRequestHandler(IEstadoRepository EstadoRepository) => _repository = EstadoRepository;

    public async Task<Result<EstadoResponse>> Handle(SelectEstadoByIdRequest request, CancellationToken cancellationToken)
    {
        var estados = await _repository.GetByIdAsync(request.Id);
        return await Task.FromResult(EstadoResponse.MapFromEntity(estados));
    }
}