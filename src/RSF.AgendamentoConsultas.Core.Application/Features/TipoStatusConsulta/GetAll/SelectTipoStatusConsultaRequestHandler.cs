using RSF.AgendamentoConsultas.Core.Application.Features.TipoStatusConsulta.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoStatusConsulta.GetAll;

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
