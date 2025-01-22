using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteByIdPlanosMedicos;

public class SelectPacientePlanosMedicosRequestHandler : IRequestHandler<SelectPacientePlanosMedicosRequest, Result<PacienteResultList<PacientePlanoMedicoResponse>>>
{
    private readonly IPacienteRepository _repository;

    public SelectPacientePlanosMedicosRequestHandler(IPacienteRepository repository) => _repository = repository;

    public async Task<Result<PacienteResultList<PacientePlanoMedicoResponse>>> Handle(SelectPacientePlanosMedicosRequest request, CancellationToken cancellationToken)
    {
        var planosMedicos = await _repository.GetPlanosMedicosPacienteByIdAsync(request.PacienteId);

        NotFoundException.ThrowIfNull(planosMedicos.Count == 0 ? null : planosMedicos, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        var response = new PacienteResultList<PacientePlanoMedicoResponse>(request.PacienteId, PacientePlanoMedicoResponse.MapFromEntity(planosMedicos));

        return Result.Success(response);
    }
}

