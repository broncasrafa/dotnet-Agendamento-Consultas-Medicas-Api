using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.GetById;

public record SelectPacienteByIdRequest(int Id) : IRequest<Result<PacienteResponse>>;