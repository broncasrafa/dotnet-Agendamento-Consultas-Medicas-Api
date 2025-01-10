using RSF.AgendamentoConsultas.Application.Handlers.Features.TipoAgendamento.Responses;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.TipoAgendamento.GetById;

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