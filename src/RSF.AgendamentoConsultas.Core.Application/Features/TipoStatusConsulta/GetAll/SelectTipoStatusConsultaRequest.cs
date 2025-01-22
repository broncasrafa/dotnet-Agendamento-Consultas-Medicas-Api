﻿using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.TipoStatusConsulta.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoStatusConsulta.GetAll;

public record SelectTipoStatusConsultaRequest : IRequest<Result<IReadOnlyList<TipoStatusConsultaResponse>>>;