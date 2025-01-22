using RSF.AgendamentoConsultas.Core.Domain.Interfaces;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.GetAll;

public class SelectConvenioMedicoRequestHandler : IRequestHandler<SelectConvenioMedicoRequest, Result<IReadOnlyList<ConvenioMedicoResponse>>>
{
    private readonly IConvenioMedicoRepository _repository;

    public SelectConvenioMedicoRequestHandler(IConvenioMedicoRepository repository) => _repository = repository;

    public async Task<Result<IReadOnlyList<ConvenioMedicoResponse>>> Handle(SelectConvenioMedicoRequest request, CancellationToken cancellationToken)
    {
        var convenios = await _repository.GetAllAsync();
        return Result.Success<IReadOnlyList<ConvenioMedicoResponse>>(ConvenioMedicoResponse.MapFromEntity(convenios));
    }
}