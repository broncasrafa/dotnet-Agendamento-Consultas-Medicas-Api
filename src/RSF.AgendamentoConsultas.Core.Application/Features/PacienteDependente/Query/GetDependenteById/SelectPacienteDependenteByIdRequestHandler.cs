using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Responses;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Query.GetDependenteById;

public class SelectPacienteDependenteByIdRequestHandler : IRequestHandler<SelectPacienteDependenteByIdRequest, Result<PacienteDependenteResponse>>
{
    private readonly IPacienteDependenteRepository _repository;

    public SelectPacienteDependenteByIdRequestHandler(IPacienteDependenteRepository repository) => _repository = repository;

    public async Task<Result<PacienteDependenteResponse>> Handle(SelectPacienteDependenteByIdRequest request, CancellationToken cancellationToken)
    {
        var dependente = await _repository.GetByIdAsync(request.DependenteId);

        NotFoundException.ThrowIfNull(dependente, $"Dependente com o ID: '{request.DependenteId}' não foi encontrado");

        return await Task.FromResult(PacienteDependenteResponse.MapFromEntity(dependente));
    }
}