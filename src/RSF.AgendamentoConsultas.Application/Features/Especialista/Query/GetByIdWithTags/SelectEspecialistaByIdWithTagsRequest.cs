﻿using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Especialista.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Especialista.Query.GetByIdWithTags;

public record SelectEspecialistaByIdWithTagsRequest(int Id) : IRequest<Result<EspecialistaResultList<EspecialistaTagsResponse>>>;