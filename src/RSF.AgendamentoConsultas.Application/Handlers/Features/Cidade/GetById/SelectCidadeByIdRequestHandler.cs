using RSF.AgendamentoConsultas.Domain.Interfaces;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Cidade.GetById;

public class SelectCidadeByIdRequestHandler(ICidadeRepository cidadeRepository) : IRequestHandler<SelectCidadeByIdRequest, Result<SelectCidadeResponse>>
{
    private readonly ICidadeRepository _repository = cidadeRepository;

    public async Task<Result<SelectCidadeResponse>> Handle(SelectCidadeByIdRequest request, CancellationToken cancellationToken)
    {
        var cidade = await _repository.GetByIdAsync(request.Id);
        return await Task.FromResult(SelectCidadeResponse.MapFromEntity(cidade));
    }
}