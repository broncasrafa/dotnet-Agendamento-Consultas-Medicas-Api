using RSF.AgendamentoConsultas.Application.Handlers.Features.TipoStatusConsulta.Responses;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.TipoStatusConsulta.GetAll;

public class SelectTipoStatusConsultaRequestHandler : IRequestHandler<SelectTipoStatusConsultaRequest, Result<IReadOnlyList<TipoStatusConsultaResponse>>>
{
    private readonly IBaseRepository<Domain.Entities.TipoStatusConsulta> _repository;

    public SelectTipoStatusConsultaRequestHandler(IBaseRepository<Domain.Entities.TipoStatusConsulta> repository) => _repository = repository;

    public async Task<Result<IReadOnlyList<TipoStatusConsultaResponse>>> Handle(SelectTipoStatusConsultaRequest request, CancellationToken cancellationToken)
    {
        var regioes = await _repository.GetAllAsync();

        return Result.Success<IReadOnlyList<TipoStatusConsultaResponse>>(TipoStatusConsultaResponse.MapFromEntity(regioes));
    }
}
