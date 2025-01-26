using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.UpdateConvenioMedico
{
    public record UpdateConvenioMedicoRequest(int Id, int EspecialistaId, int ConvenioMedicoId) : IRequest<Result<bool>>;
}