using RSF.AgendamentoConsultas.Application.Handlers.Features.ConvenioMedico.Response;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.ConvenioMedico.GetById;

public class SelectConvenioMedicoByIdRequestHandler : IRequestHandler<SelectConvenioMedicoByIdRequest, Result<ConvenioMedicoResponse>>
{
    private readonly IConvenioMedicoRepository _repository;

    public SelectConvenioMedicoByIdRequestHandler(IConvenioMedicoRepository repository) => _repository = repository;

    public async Task<Result<ConvenioMedicoResponse>> Handle(SelectConvenioMedicoByIdRequest request, CancellationToken cancellationToken)
    {
        var convenioMedico = await _repository.GetByIdAsync(request.Id);
        return await Task.FromResult(ConvenioMedicoResponse.MapFromEntity(convenioMedico));
    }
}