using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.EspecialidadeGrupo.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.EspecialidadeGrupo.GetById;

public record SelectEspecialidadeGrupoByIdRequest(int Id) : IRequest<Result<EspecialidadeGrupoResponse>>;