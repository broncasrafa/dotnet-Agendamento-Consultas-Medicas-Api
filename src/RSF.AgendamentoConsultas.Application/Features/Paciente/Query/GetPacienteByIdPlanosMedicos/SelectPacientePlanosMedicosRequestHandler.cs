using RSF.AgendamentoConsultas.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Features.Paciente.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Query.GetPacienteByIdPlanosMedicos;

public class SelectPacientePlanosMedicosRequestHandler : IRequestHandler<SelectPacientePlanosMedicosRequest, Result<PacienteResultList<PacientePlanoMedicoResponse>>>
{
    private readonly IPacienteRepository _repository;

    public SelectPacientePlanosMedicosRequestHandler(IPacienteRepository repository) => _repository = repository;

    public async Task<Result<PacienteResultList<PacientePlanoMedicoResponse>>> Handle(SelectPacientePlanosMedicosRequest request, CancellationToken cancellationToken)
    {
        var planosMedicos = await _repository.GetPlanosMedicosPacienteByIdAsync(request.Id);

        NotFoundException.ThrowIfNull(planosMedicos.Count == 0 ? null : planosMedicos, $"Paciente com o ID: '{request.Id}' não foi encontrado");

        var response = new PacienteResultList<PacientePlanoMedicoResponse>(request.Id, PacientePlanoMedicoResponse.MapFromEntity(planosMedicos));

        return Result.Success(response);
    }
}

