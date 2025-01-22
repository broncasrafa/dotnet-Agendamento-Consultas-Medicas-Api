using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Responses;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Query.GetDependenteByIdPlanosMedicos;

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