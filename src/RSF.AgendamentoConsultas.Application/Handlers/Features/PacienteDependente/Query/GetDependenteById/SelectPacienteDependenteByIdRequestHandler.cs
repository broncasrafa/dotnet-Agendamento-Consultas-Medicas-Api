using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Handlers.Features.PacienteDependente.Responses;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.PacienteDependente.Query.GetDependenteById;

public class SelectPacienteDependenteByIdRequestHandler : IRequestHandler<SelectPacienteDependenteByIdRequest, Result<PacienteDependenteResponse>>
{
    private readonly IPacienteDependenteRepository _repository;

    public SelectPacienteDependenteByIdRequestHandler(IPacienteDependenteRepository repository) => _repository = repository;

    public async Task<Result<PacienteDependenteResponse>> Handle(SelectPacienteDependenteByIdRequest request, CancellationToken cancellationToken)
    {
        var dependente = await _repository.GetByIdAsync(request.DependenteId, request.PacientePrincipalId);

        NotFoundException.ThrowIfNull(dependente, $"Dependente com o ID: '{request.DependenteId}' não foi encontrado");

        return await Task.FromResult(PacienteDependenteResponse.MapFromEntity(dependente));
    }
}