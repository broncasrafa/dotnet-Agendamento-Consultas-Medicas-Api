using RSF.AgendamentoConsultas.Application.Handlers.Features.Cidade.Response;
using MediatR;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Cidade.Request;

public class SelectCidadeRequest : IRequest<IReadOnlyList<SelectCidadeResponse>>
{
}