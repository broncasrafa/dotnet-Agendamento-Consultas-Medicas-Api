using RSF.AgendamentoConsultas.Core.Domain.Interfaces;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Command.UpdateDependente;

public class UpdatePacienteDependenteRequestHandler : IRequestHandler<UpdatePacienteDependenteRequest, Result<bool>>
{
    private readonly IPacienteDependenteRepository _dependenteRepository;
    private readonly IPacienteRepository _pacienteRepository;

    public UpdatePacienteDependenteRequestHandler(IPacienteDependenteRepository dependenteRepository, IPacienteRepository pacienteRepository)
    {
        _dependenteRepository = dependenteRepository;
        _pacienteRepository = pacienteRepository;
    }

    public async Task<Result<bool>> Handle(UpdatePacienteDependenteRequest request, CancellationToken cancellationToken)
    {
        var pacientePrincipal = await _pacienteRepository.GetByFilterAsync(c => c.PacienteId == request.PacientePrincipalId, c => c.Dependentes);
        NotFoundException.ThrowIfNull(pacientePrincipal, $"Paciente principal com o ID: '{request.PacientePrincipalId}' não foi encontrado");

        var dependente = pacientePrincipal.Dependentes.SingleOrDefault(c => c.DependenteId == request.DependenteId);
        NotFoundException.ThrowIfNull(dependente, $"Dependente com o ID: '{request.DependenteId}' não foi encontrado para o Paciente principal ID: '{request.PacientePrincipalId}'");

        dependente!.Update(request.NomeCompleto, request.Email, request.Telefone, request.Genero, request.DataNascimento.ToString("yyyy-MM-dd"), request.CPF);

        var rowsAffected = await _dependenteRepository.UpdateAsync(dependente);

        return await Task.FromResult(rowsAffected > 0);
    }
}