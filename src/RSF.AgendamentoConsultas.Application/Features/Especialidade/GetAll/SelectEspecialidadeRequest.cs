using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Especialidade.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Especialidade.GetAll;

public record SelectEspecialidadeRequest() : IRequest<Result<IReadOnlyList<EspecialidadeResponse>>>;