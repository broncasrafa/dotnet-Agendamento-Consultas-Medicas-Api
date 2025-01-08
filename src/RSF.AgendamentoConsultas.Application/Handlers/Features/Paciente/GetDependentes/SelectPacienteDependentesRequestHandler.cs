using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.GetDependentes;

public class SelectPacienteDependentesRequestHandler : IRequestHandler<SelectPacienteDependentesRequest, Result<PacienteResultList<PacienteDependenteResponse>>>
{
    private readonly IPacienteDependenteRepository _repository;

    public SelectPacienteDependentesRequestHandler(IPacienteDependenteRepository repository) => _repository = repository;


    public async Task<Result<PacienteResultList<PacienteDependenteResponse>>> Handle(SelectPacienteDependentesRequest request, CancellationToken cancellationToken)
    {
        var dependentes = await _repository.GetAllByFilterAsync(c => c.PacientePrincipalId == request.Id);

        NotFoundException.ThrowIfNull(dependentes, $"Paciente Principal com o ID: '{request.Id}' não foi encontrado");
        
        var response = new PacienteResultList<PacienteDependenteResponse>(request.Id, PacienteDependenteResponse.MapFromEntity(dependentes));

        return Result.Success<PacienteResultList<PacienteDependenteResponse>>(response);
    }
}

