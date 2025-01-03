using RSF.AgendamentoConsultas.Application.Handlers.Features.Cidade.Request;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Cidade.Response;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using MediatR;


namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Cidade.Handlers;

public class SelectCidadeRequestHandler(ICidadeRepository cidadeRepository) : IRequestHandler<SelectCidadeRequest, IReadOnlyList<SelectCidadeResponse>>
{
    private readonly ICidadeRepository _repository = cidadeRepository;

    public async Task<IReadOnlyList<SelectCidadeResponse>> Handle(SelectCidadeRequest request, CancellationToken cancellationToken)
    {
        var cidades = await _repository.GetAllAsync();
        return await Task.FromResult(SelectCidadeResponse.MapFromEntity(cidades));
    }
}