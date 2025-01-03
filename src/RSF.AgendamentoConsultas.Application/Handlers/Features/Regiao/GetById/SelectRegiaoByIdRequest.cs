using RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.GetAll;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.GetById;

public record SelectRegiaoByIdRequest(int Id): IRequest<Result<SelectRegiaoResponse>>;