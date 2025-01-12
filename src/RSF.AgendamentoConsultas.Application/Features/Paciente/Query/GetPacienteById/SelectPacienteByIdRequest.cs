using RSF.AgendamentoConsultas.Application.Features.Paciente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Query.GetPacienteById;

public record SelectPacienteByIdRequest(int PacienteId) : IRequest<Result<PacienteResponse>>;