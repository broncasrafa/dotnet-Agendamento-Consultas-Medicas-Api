using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialidade.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialidade.GetById;

public record SelectEspecialidadeByIdRequest(int Id) : IRequest<Result<EspecialidadeResponse>>;