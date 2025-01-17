﻿using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Tags.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Tags.GetById;

public record SelectTagsByIdRequest(int Id) : IRequest<Result<TagsResponse>>;