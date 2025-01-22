using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteById;

public record SelectPacienteByIdRequest(int PacienteId) : IRequest<Result<PacienteResponse>>;