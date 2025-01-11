using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Query.GetDependenteByIdPlanosMedicos;

public class SelectPacienteDependentePlanosMedicosRequestHandler : IRequestHandler<SelectPacienteDependentePlanosMedicosRequest, Result<PacienteDependenteResultList<PacienteDependentePlanoMedicoResponse>>>
{
    private readonly IPacienteDependenteRepository _repository;

    public SelectPacienteDependentePlanosMedicosRequestHandler(IPacienteDependenteRepository repository) => _repository = repository;

    public async Task<Result<PacienteDependenteResultList<PacienteDependentePlanoMedicoResponse>>> Handle(SelectPacienteDependentePlanosMedicosRequest request, CancellationToken cancellationToken)
    {
        var dependente = await _repository.GetByIdAsync(request.DependenteId);

        NotFoundException.ThrowIfNull(dependente, $"Paciente Dependente com o ID: '{request.DependenteId}' não foi encontrado");

        var response = new PacienteDependenteResultList<PacienteDependentePlanoMedicoResponse>(
            request.DependenteId,
            dependente.PacientePrincipalId,
            PacienteDependentePlanoMedicoResponse.MapFromEntity(dependente.PlanosMedicos));

        return Result.Success(response);
    }
}