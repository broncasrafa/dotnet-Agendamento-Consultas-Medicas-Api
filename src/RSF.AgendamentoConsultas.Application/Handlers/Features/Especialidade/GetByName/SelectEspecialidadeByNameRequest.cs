using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialidade.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialidade.GetByName;

public record SelectEspecialidadeByNameRequest(string Name) : IRequest<Result<IReadOnlyList<EspecialidadeResponse>>>;