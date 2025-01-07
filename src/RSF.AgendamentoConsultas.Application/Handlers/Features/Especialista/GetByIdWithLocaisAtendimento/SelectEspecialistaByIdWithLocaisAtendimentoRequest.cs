using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.GetByIdWithLocaisAtendimento;

public record SelectEspecialistaByIdWithLocaisAtendimentoRequest(int Id) : IRequest<Result<EspecialistaResultList<EspecialistaLocalAtendimentoResponse>>>;