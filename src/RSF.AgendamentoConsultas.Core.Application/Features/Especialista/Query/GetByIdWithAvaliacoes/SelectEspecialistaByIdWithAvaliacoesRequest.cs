using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithAvaliacoes;

public record SelectEspecialistaByIdWithAvaliacoesRequest(int Id) : IRequest<Result<EspecialistaResultList<EspecialistaAvaliacaoResponse>>>;