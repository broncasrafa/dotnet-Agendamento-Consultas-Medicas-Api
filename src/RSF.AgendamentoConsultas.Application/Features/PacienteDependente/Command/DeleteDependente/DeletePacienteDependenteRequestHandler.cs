using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Command.DeleteDependente;

public class DeletePacienteDependenteRequestHandler : IRequestHandler<DeletePacienteDependenteRequest, Result<bool>>
{
    private readonly IPacienteDependenteRepository _dependenteRepository;
    private readonly IPacienteRepository _pacienteRepository;

    public DeletePacienteDependenteRequestHandler(IPacienteDependenteRepository dependenteRepository, IPacienteRepository pacienteRepository)
    {
        _dependenteRepository = dependenteRepository;
        _pacienteRepository = pacienteRepository;
    }

    public async Task<Result<bool>> Handle(DeletePacienteDependenteRequest request, CancellationToken cancellationToken)
    {
        var pacientePrincipal = await _pacienteRepository.GetByFilterAsync(c => c.PacienteId == request.PacientePrincipalId, c => c.Dependentes);
        NotFoundException.ThrowIfNull(pacientePrincipal, $"Paciente principal com o ID: '{request.PacientePrincipalId}' não foi encontrado");

        var dependente = pacientePrincipal.Dependentes.SingleOrDefault(c => c.DependenteId == request.DependenteId);
        NotFoundException.ThrowIfNull(dependente, $"Dependente com o ID: '{request.DependenteId}' não foi encontrado para o Paciente principal ID: '{request.PacientePrincipalId}'");

        dependente!.ChangeStatus(status: false);

        var rowsAffected = await _dependenteRepository.ChangeStatusAsync(dependente);

        return await Task.FromResult(rowsAffected > 0);
    }
}