using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Especialidade.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Especialidade.GetByName;

public record SelectEspecialidadeByNameRequest(string Name) : IRequest<Result<IReadOnlyList<EspecialidadeResponse>>>;