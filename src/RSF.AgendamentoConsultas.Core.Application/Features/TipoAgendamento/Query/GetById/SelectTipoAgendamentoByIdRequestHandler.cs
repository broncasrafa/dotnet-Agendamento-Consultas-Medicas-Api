using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Application.Features.TipoAgendamento.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoAgendamento.Query.GetById;

public class SelectTipoAgendamentoByIdRequestHandler : IRequestHandler<SelectTipoAgendamentoByIdRequest, Result<TipoAgendamentoResponse>>
{
    private readonly IBaseRepository<Domain.Entities.TipoAgendamento> _repository;

    public SelectTipoAgendamentoByIdRequestHandler(IBaseRepository<Domain.Entities.TipoAgendamento> repository) => _repository = repository;

    public async Task<Result<TipoAgendamentoResponse>> Handle(SelectTipoAgendamentoByIdRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByIdAsync(request.Id);

        NotFoundException.ThrowIfNull(data, $"Tipo de Agendamento com o ID: '{request.Id}' não encontrada");

        return await Task.FromResult(TipoAgendamentoResponse.MapFromEntity(data));
    }
}