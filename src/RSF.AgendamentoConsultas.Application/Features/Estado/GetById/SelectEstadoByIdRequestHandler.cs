using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Features.Estado.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Estado.GetById;

public class SelectEstadoByIdRequestHandler : IRequestHandler<SelectEstadoByIdRequest, Result<EstadoResponse>>
{
    private readonly IEstadoRepository _repository;

    public SelectEstadoByIdRequestHandler(IEstadoRepository EstadoRepository) => _repository = EstadoRepository;

    public async Task<Result<EstadoResponse>> Handle(SelectEstadoByIdRequest request, CancellationToken cancellationToken)
    {
        var estado = await _repository.GetByIdAsync(request.Id);
        NotFoundException.ThrowIfNull(estado, $"Estado com o ID: '{request.Id}' não encontrado");
        return await Task.FromResult(EstadoResponse.MapFromEntity(estado));
    }
}