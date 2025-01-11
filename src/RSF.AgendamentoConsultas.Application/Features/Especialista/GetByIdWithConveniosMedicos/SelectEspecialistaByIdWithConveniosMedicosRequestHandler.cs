using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Features.Especialista.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Especialista.GetByIdWithConveniosMedicos;

public class SelectEspecialistaByIdWithConveniosMedicosRequestHandler : IRequestHandler<SelectEspecialistaByIdWithConveniosMedicosRequest, Result<EspecialistaResultList<EspecialistaConvenioMedicoResponse>>>
{
    private readonly IEspecialistaRepository _repository;

    public SelectEspecialistaByIdWithConveniosMedicosRequestHandler(IEspecialistaRepository repository) => _repository = repository;

    public async Task<Result<EspecialistaResultList<EspecialistaConvenioMedicoResponse>>> Handle(SelectEspecialistaByIdWithConveniosMedicosRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByIdWithConveniosMedicosAsync(request.Id);

        NotFoundException.ThrowIfNull(data, $"Especialista com o ID: '{request.Id}' não encontrado");

        var response = new EspecialistaResultList<EspecialistaConvenioMedicoResponse>(data.EspecialistaId, EspecialistaConvenioMedicoResponse.MapFromEntity(data.ConveniosMedicosAtendidos?.Select(c => c.ConvenioMedico)));

        return Result.Success(response);
    }
}