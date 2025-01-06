using RSF.AgendamentoConsultas.Application.Handlers.Features.ConvenioMedico.Responses;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.ConvenioMedico.GetAll;

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