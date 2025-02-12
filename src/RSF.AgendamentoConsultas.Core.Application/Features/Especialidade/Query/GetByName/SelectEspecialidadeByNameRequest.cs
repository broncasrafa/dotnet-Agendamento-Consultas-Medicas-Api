using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Especialidade.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialidade.Query.GetByName;

public record SelectEspecialidadeByNameRequest(string Name) : IRequest<Result<IReadOnlyList<EspecialidadeResponse>>>;