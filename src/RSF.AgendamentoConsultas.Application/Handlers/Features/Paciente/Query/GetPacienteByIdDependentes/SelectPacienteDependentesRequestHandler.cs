using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Handlers.Features.PacienteDependente.Responses;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Query.GetPacienteByIdDependentes;

public class SelectPacienteDependentesRequestHandler : IRequestHandler<SelectPacienteDependentesRequest, Result<PacienteResultList<PacienteDependenteResponse>>>
{
    private readonly IPacienteRepository _repository;

    public SelectPacienteDependentesRequestHandler(IPacienteRepository repository) => _repository = repository;


    public async Task<Result<PacienteResultList<PacienteDependenteResponse>>> Handle(SelectPacienteDependentesRequest request, CancellationToken cancellationToken)
    {
        var dependentes = await _repository.GetDependentesPacienteByIdAsync(request.Id);

        NotFoundException.ThrowIfNull(dependentes.Count == 0 ? null : dependentes, $"Paciente Principal com o ID: '{request.Id}' não foi encontrado");

        var response = new PacienteResultList<PacienteDependenteResponse>(request.Id, PacienteDependenteResponse.MapFromEntity(dependentes));

        return Result.Success(response);
    }
}

