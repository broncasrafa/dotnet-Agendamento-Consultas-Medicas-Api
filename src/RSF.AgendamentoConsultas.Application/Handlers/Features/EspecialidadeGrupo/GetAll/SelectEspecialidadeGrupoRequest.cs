using RSF.AgendamentoConsultas.Application.Handlers.Features.EspecialidadeGrupo.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.EspecialidadeGrupo.GetAll;

public record SelectEspecialidadeGrupoRequest : IRequest<Result<IReadOnlyList<EspecialidadeGrupoResponse>>>;