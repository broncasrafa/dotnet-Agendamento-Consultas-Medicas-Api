using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.EspecialidadeGrupo.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.EspecialidadeGrupo.Query.GetById;

public record SelectEspecialidadeGrupoByIdRequest(int Id) : IRequest<Result<EspecialidadeGrupoResponse>>;