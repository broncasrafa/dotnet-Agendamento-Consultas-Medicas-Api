using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Command.DeletePaciente;

public class DeletePacienteRequestHandler : IRequestHandler<DeletePacienteRequest, Result<bool>>
{
    private readonly IPacienteRepository _repository;

    public DeletePacienteRequestHandler(IPacienteRepository repository) => _repository = repository;

    public async Task<Result<bool>> Handle(DeletePacienteRequest request, CancellationToken cancellationToken)
    {
        var paciente = await _repository.GetByIdAsync(request.PacienteId);

        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        paciente.ChangeStatus(status: false);

        var rowsAffected = await _repository.ChangeStatusAsync(paciente);

        return await Task.FromResult(rowsAffected > 0);
    }
}