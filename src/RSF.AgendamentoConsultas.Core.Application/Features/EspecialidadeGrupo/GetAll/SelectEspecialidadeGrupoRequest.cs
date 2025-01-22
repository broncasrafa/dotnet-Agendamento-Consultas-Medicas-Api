﻿using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.EspecialidadeGrupo.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.EspecialidadeGrupo.GetAll;

public record SelectEspecialidadeGrupoRequest : IRequest<Result<IReadOnlyList<EspecialidadeGrupoResponse>>>;