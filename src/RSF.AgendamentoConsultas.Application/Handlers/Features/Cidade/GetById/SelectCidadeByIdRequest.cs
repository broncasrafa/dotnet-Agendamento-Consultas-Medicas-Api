using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Cidade.GetById;

public record SelectCidadeByIdRequest(int Id) : IRequest<Result<SelectCidadeResponse>>;