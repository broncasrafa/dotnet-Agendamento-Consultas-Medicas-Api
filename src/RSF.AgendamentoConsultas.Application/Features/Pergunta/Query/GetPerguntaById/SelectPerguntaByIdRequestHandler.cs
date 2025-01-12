using RSF.AgendamentoConsultas.Application.Features.Pergunta.Responses;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using MediatR;
using OperationResult;


namespace RSF.AgendamentoConsultas.Application.Features.Pergunta.Query.GetPerguntaById;

public class SelectPerguntaByIdRequestHandler : IRequestHandler<SelectPerguntaByIdRequest, Result<PerguntaResponse>>
{
    private readonly IBaseRepository<Domain.Entities.Pergunta> _especialistaPerguntaRepository;

    public SelectPerguntaByIdRequestHandler(IBaseRepository<Domain.Entities.Pergunta> especialistaPerguntaRepository) 
        => _especialistaPerguntaRepository = especialistaPerguntaRepository;

    public async Task<Result<PerguntaResponse>> Handle(SelectPerguntaByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await _especialistaPerguntaRepository.GetByFilterAsync(c => c.PerguntaId == request.Id, c => c.Especialidade, c => c.Paciente);

        NotFoundException.ThrowIfNull(result, $"Pergunta com o ID: '{request.Id}' não encontrada");

        return await Task.FromResult(PerguntaResponse.MapFromEntity(result));
    }
}