using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.EspecialidadeGrupo.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.EspecialidadeGrupo.GetAll;

public record SelectEspecialidadeGrupoRequest : IRequest<Result<IReadOnlyList<EspecialidadeGrupoResponse>>>;