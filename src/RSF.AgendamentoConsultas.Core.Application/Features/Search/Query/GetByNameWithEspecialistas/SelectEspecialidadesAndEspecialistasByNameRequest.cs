using RSF.AgendamentoConsultas.Core.Application.Features.Search.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Search.Query.GetByNameWithEspecialistas;

public record SelectEspecialidadesAndEspecialistasByNameRequest(string Term)
    : IRequest<Result<SearchEspecialidadesAndEspecialistasByNameResponse>>;
