using RSF.AgendamentoConsultas.Core.Application.Features.Cidade.Responses;
using MediatR;
using OperationResult;


namespace RSF.AgendamentoConsultas.Core.Application.Features.Cidade.Query.SearchByName;

public record SelectCidadesByNameRequest(string Name) : IRequest<Result<IReadOnlyList<CidadeResponse>>>;