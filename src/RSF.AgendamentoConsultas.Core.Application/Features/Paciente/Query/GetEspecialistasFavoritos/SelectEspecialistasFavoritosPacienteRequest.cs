using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetEspecialistasFavoritos;

public record SelectEspecialistasFavoritosPacienteRequest(int PacienteId, int PageSize = 15, int PageNum = 1) 
    : IRequest<Result<PagedResult<EspecialistaResponse>>>;