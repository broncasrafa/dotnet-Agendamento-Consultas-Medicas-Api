﻿using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Regiao.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Regiao.GetById;

public record SelectRegiaoByIdRequest(int Id) : IRequest<Result<RegiaoResponse>>;