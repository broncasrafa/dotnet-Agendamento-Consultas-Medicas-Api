using RSF.AgendamentoConsultas.Application.Features.Especialista.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Especialista.Query.GetEspecialistaRespostas;

public record SelectEspecialistaRespostasRequest(int EspecialistaId) : IRequest<Result<EspecialistaResultList<EspecialistaPerguntaRespostaResponse>>>;