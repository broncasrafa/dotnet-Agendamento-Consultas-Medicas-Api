using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.TipoConsulta.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoConsulta.Query.GetAll;

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
