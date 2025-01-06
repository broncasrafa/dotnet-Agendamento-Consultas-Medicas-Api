using RSF.AgendamentoConsultas.Application.Handlers.Features.TipoConsulta.Responses;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.TipoConsulta.GetById;

public class SelectTipoConsultaByIdRequestHandler : IRequestHandler<SelectTipoConsultaByIdRequest, Result<TipoConsultaResponse>>
{
    private readonly IBaseRepository<Domain.Entities.TipoConsulta> _repository;

    public SelectTipoConsultaByIdRequestHandler(IBaseRepository<Domain.Entities.TipoConsulta> repository) => _repository = repository;

    public async Task<Result<TipoConsultaResponse>> Handle(SelectTipoConsultaByIdRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByIdAsync(request.Id);

        NotFoundException.ThrowIfNull(data, $"Tipo de Consulta com o ID: '{request.Id}' não encontrada");

        return await Task.FromResult(TipoConsultaResponse.MapFromEntity(data));
    }
}