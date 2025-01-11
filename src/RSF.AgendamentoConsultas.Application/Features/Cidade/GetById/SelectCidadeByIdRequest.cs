using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Cidade.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Cidade.GetById;

public record SelectCidadeByIdRequest(int Id) : IRequest<Result<CidadeResponse>>;