using RSF.AgendamentoConsultas.Application.Handlers.Features.Avaliacao.Responses;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using MediatR;
using OperationResult;


namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Avaliacao.Query.GetAvaliacaoById;

public class SelectAvaliacaoByIdRequestHandler : IRequestHandler<SelectAvaliacaoByIdRequest, Result<AvaliacaoResponse>>
{
    private readonly IBaseRepository<EspecialistaAvaliacao> _especialistaAvaliacaoRepository;

    public SelectAvaliacaoByIdRequestHandler(IBaseRepository<EspecialistaAvaliacao> especialistaAvaliacaoRepository) => _especialistaAvaliacaoRepository = especialistaAvaliacaoRepository;

    public async Task<Result<AvaliacaoResponse>> Handle(SelectAvaliacaoByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await _especialistaAvaliacaoRepository.GetByFilterAsync(c => c.Id == request.Id, c => c.Especialista, c => c.Paciente);

        NotFoundException.ThrowIfNull(result, $"Avaliação com o ID: '{request.Id}' não encontrada");

        return await Task.FromResult(AvaliacaoResponse.MapFromEntity(result));
    }
}