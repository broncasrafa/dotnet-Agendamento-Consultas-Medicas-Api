using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithLocaisAtendimento;

public record SelectEspecialistaByIdWithLocaisAtendimentoRequest(int Id) : IRequest<Result<EspecialistaResultList<EspecialistaLocalAtendimentoResponse>>>;