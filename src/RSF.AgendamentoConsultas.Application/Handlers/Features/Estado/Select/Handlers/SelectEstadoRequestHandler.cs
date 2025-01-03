using RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.Request;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.Response;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using MediatR;


namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.Handlers;

public class SelectCidadeRequestHandler : IRequestHandler<SelectCidadeRequest, IReadOnlyList<SelectEstadoResponse>>
{
    private readonly IEstadoRepository _repository;

    public SelectCidadeRequestHandler(IEstadoRepository EstadoRepository) => _repository = EstadoRepository;

    public async Task<IReadOnlyList<SelectEstadoResponse>> Handle(SelectCidadeRequest request, CancellationToken cancellationToken)
    {
        var estados = await _repository.GetAllAsync();
        return await Task.FromResult(SelectEstadoResponse.MapFromEntity(estados));
    }
}