using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Handlers.Features.Cidade.Response;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Cidade.GetById;

public record SelectCidadeByIdRequest(int Id) : IRequest<Result<CidadeResponse>>;