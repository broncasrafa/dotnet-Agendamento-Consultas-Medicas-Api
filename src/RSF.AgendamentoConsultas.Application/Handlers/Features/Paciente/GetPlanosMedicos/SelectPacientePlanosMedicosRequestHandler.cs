using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.GetPlanosMedicos;

public class SelectPacientePlanosMedicosRequestHandler : IRequestHandler<SelectPacientePlanosMedicosRequest, Result<PacienteResultList<PacientePlanoMedicoResponse>>>
{
    private readonly IPacienteRepository _repository;

    public SelectPacientePlanosMedicosRequestHandler(IPacienteRepository repository) => _repository = repository;

    public async Task<Result<PacienteResultList<PacientePlanoMedicoResponse>>> Handle(SelectPacientePlanosMedicosRequest request, CancellationToken cancellationToken)
    {
        var planos = await _repository.GetByIdWithPlanosMedicosAsync(request.Id);

        NotFoundException.ThrowIfNull(planos, $"Paciente com o ID: '{request.Id}' não foi encontrado");
        
        var response = new PacienteResultList<PacientePlanoMedicoResponse>(request.Id, PacientePlanoMedicoResponse.MapFromEntity(planos));

        return Result.Success<PacienteResultList<PacientePlanoMedicoResponse>>(response);
    }
}

