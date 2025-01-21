using RSF.AgendamentoConsultas.Application.Features.TipoStatusConsulta.Responses;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.TipoStatusConsulta.GetAll;

public class SelectTipoStatusConsultaRequestHandler : IRequestHandler<SelectTipoStatusConsultaRequest, Result<IReadOnlyList<TipoStatusConsultaResponse>>>
{
    private readonly ITipoStatusConsultaRepository _repository;

    public SelectTipoStatusConsultaRequestHandler(ITipoStatusConsultaRepository repository) => _repository = repository;

    public async Task<Result<IReadOnlyList<TipoStatusConsultaResponse>>> Handle(SelectTipoStatusConsultaRequest request, CancellationToken cancellationToken)
    {
        var regioes = await _repository.GetAllAsync();

        return Result.Success(TipoStatusConsultaResponse.MapFromEntity(regioes));
    }
}
