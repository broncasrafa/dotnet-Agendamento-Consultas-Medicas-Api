﻿using RSF.AgendamentoConsultas.Application.Handlers.Features.PerguntasRespostas.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.PerguntasRespostas.Query.GetRespostasByPerguntaId;

public record SelectRespostaByIdRequest(int Id) : IRequest<Result<RespostaResponse>>;