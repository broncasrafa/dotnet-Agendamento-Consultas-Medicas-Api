using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Query.GetPacienteById;

public record SelectPacienteByIdRequest(int Id) : IRequest<Result<PacienteResponse>>;