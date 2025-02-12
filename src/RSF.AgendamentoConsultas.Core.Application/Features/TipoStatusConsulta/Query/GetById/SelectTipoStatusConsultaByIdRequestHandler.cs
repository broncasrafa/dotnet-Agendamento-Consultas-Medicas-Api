using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Application.Features.TipoStatusConsulta.Responses;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoStatusConsulta.Query.GetById;

public class SelectTipoStatusConsultaByIdRequestHandler : IRequestHandler<SelectTipoStatusConsultaByIdRequest, Result<TipoStatusConsultaResponse>>
{
    private readonly ITipoStatusConsultaRepository _repository;

    public SelectTipoStatusConsultaByIdRequestHandler(ITipoStatusConsultaRepository repository) => _repository = repository;

    public async Task<Result<TipoStatusConsultaResponse>> Handle(SelectTipoStatusConsultaByIdRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByIdAsync(request.Id);

        NotFoundException.ThrowIfNull(data, $"Tipo de Status da Consulta com o ID: '{request.Id}' não encontrada");

        return await Task.FromResult(TipoStatusConsultaResponse.MapFromEntity(data));
    }
}