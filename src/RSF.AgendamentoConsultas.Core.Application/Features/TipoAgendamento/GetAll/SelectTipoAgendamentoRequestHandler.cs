using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Common;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Application.Features.TipoAgendamento.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoAgendamento.GetAll;

public class SelectTipoAgendamentoRequestHandler : IRequestHandler<SelectTipoAgendamentoRequest, Result<IReadOnlyList<TipoAgendamentoResponse>>>
{
    private readonly IBaseRepository<Domain.Entities.TipoAgendamento> _repository;

    public SelectTipoAgendamentoRequestHandler(IBaseRepository<Domain.Entities.TipoAgendamento> repository) => _repository = repository;

    public async Task<Result<IReadOnlyList<TipoAgendamentoResponse>>> Handle(SelectTipoAgendamentoRequest request, CancellationToken cancellationToken)
    {
        var regioes = await _repository.GetAllAsync();

        return Result.Success(TipoAgendamentoResponse.MapFromEntity(regioes));
    }
}
