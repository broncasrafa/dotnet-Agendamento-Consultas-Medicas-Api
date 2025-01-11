using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Paciente.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Query.GetPacienteById;

public record SelectPacienteByIdRequest(int Id) : IRequest<Result<PacienteResponse>>;