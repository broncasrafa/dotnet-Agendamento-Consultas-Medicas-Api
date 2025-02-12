using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Especialidade.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialidade.Query.GetAll;

public record SelectEspecialidadeRequest() : IRequest<Result<IReadOnlyList<EspecialidadeResponse>>>;