using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithEspecialidades;

public record SelectEspecialistaByIdWithEspecialidadesRequest(int Id) : IRequest<Result<EspecialistaResultList<EspecialistaEspecialidadeResponse>>>;