using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialidade.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialidade.GetAll;

public record SelectEspecialidadeRequest() : IRequest<Result<IReadOnlyList<EspecialidadeResponse>>>;