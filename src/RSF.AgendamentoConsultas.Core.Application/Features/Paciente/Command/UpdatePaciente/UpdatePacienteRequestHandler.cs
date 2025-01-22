using RSF.AgendamentoConsultas.Core.Domain.Interfaces;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.UpdatePaciente;

public class UpdatePacienteRequestHandler : IRequestHandler<UpdatePacienteRequest, Result<bool>>
{
    private readonly IPacienteRepository _repository;

    public UpdatePacienteRequestHandler(IPacienteRepository repository) => _repository = repository;

    public async Task<Result<bool>> Handle(UpdatePacienteRequest request, CancellationToken cancellationToken)
    {
        var paciente = await _repository.GetByIdAsync(request.PacienteId);

        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        paciente.Update(request.NomeCompleto, request.Email, request.Telefone, request.Genero, request.DataNascimento.ToString("yyyy-MM-dd"));

        var rowsAffected = await _repository.UpdateAsync(paciente);

        return await Task.FromResult(rowsAffected > 0);
    }
}