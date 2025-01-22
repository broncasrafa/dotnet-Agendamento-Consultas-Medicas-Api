using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Application.Features.TipoConsulta.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoConsulta.GetById;

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