using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.DeleteConvenioMedico;

public record DeleteConvenioMedicoRequest(int EspecialistaId, int ConvenioMedicoId) : IRequest<Result<bool>>;