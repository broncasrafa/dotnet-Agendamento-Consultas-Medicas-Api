using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Especialista.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Especialista.GetByIdWithPerguntasRespostas;

public record SelectEspecialistaByIdWithPerguntasRespostasRequest(int Id) : IRequest<Result<EspecialistaResultList<EspecialistaPerguntaResponse>>>;