using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.GetByIdWithEspecialidades;

public record SelectEspecialistaByIdWithEspecialidadesRequest(int Id) : IRequest<Result<EspecialistaResultList<EspecialistaEspecialidadeResponse>>>;