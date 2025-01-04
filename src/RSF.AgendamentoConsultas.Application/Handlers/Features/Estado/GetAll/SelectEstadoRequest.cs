﻿using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.GetAll;

public record SelectEstadoRequest() : IRequest<Result<IReadOnlyList<SelectEstadoResponse>>>;