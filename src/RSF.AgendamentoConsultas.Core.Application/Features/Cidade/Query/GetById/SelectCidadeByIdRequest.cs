using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Cidade.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Cidade.Query.GetById;

public record SelectCidadeByIdRequest(int Id) : IRequest<Result<CidadeResponse>>;