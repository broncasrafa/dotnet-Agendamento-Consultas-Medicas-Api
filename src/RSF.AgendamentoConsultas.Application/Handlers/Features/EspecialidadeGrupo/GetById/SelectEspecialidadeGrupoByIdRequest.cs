using RSF.AgendamentoConsultas.Application.Handlers.Features.EspecialidadeGrupo.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.EspecialidadeGrupo.GetById;

public record SelectEspecialidadeGrupoByIdRequest(int Id) : IRequest<Result<EspecialidadeGrupoResponse>>;