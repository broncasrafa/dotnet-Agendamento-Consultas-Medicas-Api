using RSF.AgendamentoConsultas.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Application.Features.TipoStatusConsulta.Responses;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.TipoStatusConsulta.GetById;

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