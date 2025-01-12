using RSF.AgendamentoConsultas.Application.Features.Paciente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Query.GetPacienteByIdAvaliacoes;

public record SelectPacienteAvaliacoesRequest(int PacienteId): IRequest<Result<PacienteResultList<PacienteAvaliacaoResponse>>>;