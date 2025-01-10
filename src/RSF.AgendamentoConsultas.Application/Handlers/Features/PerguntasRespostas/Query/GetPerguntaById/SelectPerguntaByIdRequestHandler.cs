using RSF.AgendamentoConsultas.Application.Handlers.Features.PerguntasRespostas.Responses;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using MediatR;
using OperationResult;


namespace RSF.AgendamentoConsultas.Application.Handlers.Features.PerguntasRespostas.Query.GetPerguntaById;

public class SelectPerguntaByIdRequestHandler : IRequestHandler<SelectPerguntaByIdRequest, Result<PerguntaResponse>>
{
    private readonly IBaseRepository<EspecialistaPergunta> _especialistaPerguntaRepository;

    public SelectPerguntaByIdRequestHandler(IBaseRepository<EspecialistaPergunta> especialistaPerguntaRepository) => _especialistaPerguntaRepository = especialistaPerguntaRepository;

    public async Task<Result<PerguntaResponse>> Handle(SelectPerguntaByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await _especialistaPerguntaRepository.GetByFilterAsync(c => c.Id == request.Id, c => c.Especialista, c => c.Paciente);

        NotFoundException.ThrowIfNull(result, $"Pergunta com o ID: '{request.Id}' não encontrada");

        return await Task.FromResult(PerguntaResponse.MapFromEntity(result));
    }
}