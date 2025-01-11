using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Especialista.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Especialista.GetByIdWithAvaliacoes;

public record SelectEspecialistaByIdWithAvaliacoesRequest(int Id) : IRequest<Result<EspecialistaResultList<EspecialistaAvaliacaoResponse>>>;