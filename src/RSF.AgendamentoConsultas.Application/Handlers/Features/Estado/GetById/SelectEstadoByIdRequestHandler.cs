using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.GetAll;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.GetById;

public class SelectEstadoByIdRequestHandler : IRequestHandler<SelectEstadoByIdRequest, Result<SelectEstadoResponse>>
{
    private readonly IEstadoRepository _repository;

    public SelectEstadoByIdRequestHandler(IEstadoRepository EstadoRepository) => _repository = EstadoRepository;

    public async Task<Result<SelectEstadoResponse>> Handle(SelectEstadoByIdRequest request, CancellationToken cancellationToken)
    {
        var estados = await _repository.GetByIdAsync(request.Id);
        return await Task.FromResult(SelectEstadoResponse.MapFromEntity(estados));
    }
}