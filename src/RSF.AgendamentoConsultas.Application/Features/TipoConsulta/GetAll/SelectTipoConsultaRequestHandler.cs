using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Features.TipoConsulta.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.TipoConsulta.GetAll;

public class SelectTipoConsultaRequestHandler : IRequestHandler<SelectTipoConsultaRequest, Result<IReadOnlyList<TipoConsultaResponse>>>
{
    private readonly IBaseRepository<Domain.Entities.TipoConsulta> _repository;

    public SelectTipoConsultaRequestHandler(IBaseRepository<Domain.Entities.TipoConsulta> repository) => _repository = repository;

    public async Task<Result<IReadOnlyList<TipoConsultaResponse>>> Handle(SelectTipoConsultaRequest request, CancellationToken cancellationToken)
    {
        var regioes = await _repository.GetAllAsync();

        return Result.Success(TipoConsultaResponse.MapFromEntity(regioes));
    }
}
