using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Features.TipoStatusConsulta.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.TipoStatusConsulta.GetAll;

public class SelectTipoStatusConsultaRequestHandler : IRequestHandler<SelectTipoStatusConsultaRequest, Result<IReadOnlyList<TipoStatusConsultaResponse>>>
{
    private readonly IBaseRepository<Domain.Entities.TipoStatusConsulta> _repository;

    public SelectTipoStatusConsultaRequestHandler(IBaseRepository<Domain.Entities.TipoStatusConsulta> repository) => _repository = repository;

    public async Task<Result<IReadOnlyList<TipoStatusConsultaResponse>>> Handle(SelectTipoStatusConsultaRequest request, CancellationToken cancellationToken)
    {
        var regioes = await _repository.GetAllAsync();

        return Result.Success(TipoStatusConsultaResponse.MapFromEntity(regioes));
    }
}
