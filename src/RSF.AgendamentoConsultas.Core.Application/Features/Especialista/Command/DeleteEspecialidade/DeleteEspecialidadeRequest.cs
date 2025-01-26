using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.DeleteEspecialidade
{
    public record DeleteEspecialidadeRequest(int EspecialistaId, int EspecialidadeId) : IRequest<Result<bool>>;
}