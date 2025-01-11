using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Especialista.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Especialista.GetByIdWithLocaisAtendimento;

public record SelectEspecialistaByIdWithLocaisAtendimentoRequest(int Id) : IRequest<Result<EspecialistaResultList<EspecialistaLocalAtendimentoResponse>>>;