using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.AddEspecialidade;

public record AddEspecialidadeRequest(int EspecialistaId, int EspecialidadeId, string TipoEspecialidade) : IRequest<Result<bool>>;