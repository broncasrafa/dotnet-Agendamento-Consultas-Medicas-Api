using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.DeleteLocalAtendimento;

public record DeleteLocalAtendimentoRequest(int LocalAtendimentoId, int EspecialistaId) : IRequest<Result<bool>>;