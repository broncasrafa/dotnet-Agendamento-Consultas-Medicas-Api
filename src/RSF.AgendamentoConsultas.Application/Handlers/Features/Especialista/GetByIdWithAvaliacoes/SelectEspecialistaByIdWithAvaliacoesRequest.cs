using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.GetByIdWithAvaliacoes;

public record SelectEspecialistaByIdWithAvaliacoesRequest(int Id) : IRequest<Result<EspecialistaResultList<EspecialistaAvaliacaoResponse>>>;