using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Especialidade.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialidade.GetById;

public record SelectEspecialidadeByIdRequest(int Id) : IRequest<Result<EspecialidadeResponse>>;