using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.UpdateEspecialidade;

public record UpdateEspecialidadeRequest(int Id, int EspecialistaId, int EspecialidadeId, string TipoEspecialidade) : IRequest<Result<bool>>;