using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.AddConvenioMedico;

public record AddConvenioMedicoRequest(int EspecialistaId, int ConvenioMedicoId) : IRequest<Result<bool>>;