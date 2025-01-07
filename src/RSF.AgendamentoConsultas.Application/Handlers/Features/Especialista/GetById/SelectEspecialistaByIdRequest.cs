using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.GetById;

public record SelectEspecialistaByIdRequest(int Id) : IRequest<Result<EspecialistaResponse>>;