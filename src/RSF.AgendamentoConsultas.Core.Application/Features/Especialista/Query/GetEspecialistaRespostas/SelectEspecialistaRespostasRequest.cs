using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetEspecialistaRespostas;

public record SelectEspecialistaRespostasRequest(int EspecialistaId) : IRequest<Result<EspecialistaResultList<EspecialistaPerguntaRespostaResponse>>>;