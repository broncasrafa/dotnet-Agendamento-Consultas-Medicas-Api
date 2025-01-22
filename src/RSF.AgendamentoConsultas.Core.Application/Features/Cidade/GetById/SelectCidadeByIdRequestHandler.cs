using RSF.AgendamentoConsultas.Core.Domain.Interfaces;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Application.Features.Cidade.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Cidade.GetById;

public class SelectCidadeByIdRequestHandler(ICidadeRepository cidadeRepository) : IRequestHandler<SelectCidadeByIdRequest, Result<CidadeResponse>>
{
    private readonly ICidadeRepository _repository = cidadeRepository;

    public async Task<Result<CidadeResponse>> Handle(SelectCidadeByIdRequest request, CancellationToken cancellationToken)
    {
        var cidade = await _repository.GetByIdAsync(request.Id);
        NotFoundException.ThrowIfNull(cidade, $"Cidade com o ID: '{request.Id}' não encontrada");
        return await Task.FromResult(CidadeResponse.MapFromEntity(cidade));
    }
}