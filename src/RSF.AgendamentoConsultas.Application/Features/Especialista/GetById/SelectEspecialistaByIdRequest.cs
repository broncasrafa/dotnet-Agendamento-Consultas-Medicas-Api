using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Especialista.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Especialista.GetById;

public record SelectEspecialistaByIdRequest(int Id) : IRequest<Result<EspecialistaResponse>>;