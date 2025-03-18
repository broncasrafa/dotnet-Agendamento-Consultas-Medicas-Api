using RSF.AgendamentoConsultas.Core.Application.Features.Cidade.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Cidade.Query.GetAll;

public record SelectCidadesRequest : IRequest<Result<IReadOnlyList<CidadeResponse>>>;
