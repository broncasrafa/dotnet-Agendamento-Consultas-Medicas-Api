using RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.Response;
using MediatR;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.Request;

public class SelectCidadeRequest : IRequest<IReadOnlyList<SelectEstadoResponse>>
{
}