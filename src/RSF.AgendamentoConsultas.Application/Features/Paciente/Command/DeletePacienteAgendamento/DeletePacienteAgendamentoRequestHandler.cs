using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Command.DeletePacienteAgendamento;

public class DeletePacienteAgendamentoRequestHandler : IRequestHandler<DeletePacienteAgendamentoRequest, Result<bool>>
{
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IAgendamentoConsultaRepository _agendamentoConsultaRepository;

    public DeletePacienteAgendamentoRequestHandler(IPacienteRepository pacienteRepository, IAgendamentoConsultaRepository agendamentoConsultaRepository)
    {
        _pacienteRepository = pacienteRepository;
        _agendamentoConsultaRepository = agendamentoConsultaRepository;
    }

    public async Task<Result<bool>> Handle(DeletePacienteAgendamentoRequest request, CancellationToken cancellationToken)
    {
        var paciente = await _pacienteRepository.GetByIdAsync(request.PacienteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        var agendamentoPaciente = await _agendamentoConsultaRepository.GetByIdAsync(request.AgendamentoId, request.PacienteId);
        NotFoundException.ThrowIfNull(agendamentoPaciente, $"Agendamento com o ID: '{request.AgendamentoId}' não foi encontrado para o Paciente com o ID: '{request.PacienteId}'");

        agendamentoPaciente.Cancelar(request.NotaCancelamento);

        var rowsAffected = await _agendamentoConsultaRepository.UpdateAsync(agendamentoPaciente);


        // disparar uma mensagem para o paciente e para o especialista informando que a consulta foi cancelada

        return await Task.FromResult(rowsAffected > 0);
    }
}