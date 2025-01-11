using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Especialidade.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Especialidade.GetById;

public record SelectEspecialidadeByIdRequest(int Id) : IRequest<Result<EspecialidadeResponse>>;