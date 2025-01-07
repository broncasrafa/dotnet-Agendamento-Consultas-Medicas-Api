using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.GetByIdWithPerguntasRespostas;

public record SelectEspecialistaByIdWithPerguntasRespostasRequest(int Id) : IRequest<Result<EspecialistaResultList<EspecialistaPerguntaResponse>>>;