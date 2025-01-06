using RSF.AgendamentoConsultas.Application.Handlers.Features.TipoStatusConsulta.Responses;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.TipoStatusConsulta.GetById;

public class SelectTipoStatusConsultaByIdRequestHandler : IRequestHandler<SelectTipoStatusConsultaByIdRequest, Result<TipoStatusConsultaResponse>>
{
    private readonly IBaseRepository<Domain.Entities.TipoStatusConsulta> _repository;

    public SelectTipoStatusConsultaByIdRequestHandler(IBaseRepository<Domain.Entities.TipoStatusConsulta> repository) => _repository = repository;

    public async Task<Result<TipoStatusConsultaResponse>> Handle(SelectTipoStatusConsultaByIdRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByIdAsync(request.Id);

        NotFoundException.ThrowIfNull(data, $"Tipo de Status da Consulta com o ID: '{request.Id}' não encontrada");

        return await Task.FromResult(TipoStatusConsultaResponse.MapFromEntity(data));
    }
}