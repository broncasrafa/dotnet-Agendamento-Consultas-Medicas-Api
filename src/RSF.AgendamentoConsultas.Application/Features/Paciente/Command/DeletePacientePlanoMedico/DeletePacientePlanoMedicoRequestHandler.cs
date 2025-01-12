using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Command.DeletePacientePlanoMedico;

public class DeletePacientePlanoMedicoRequestHandler : IRequestHandler<DeletePacientePlanoMedicoRequest, Result<bool>>
{
    private readonly IPacienteRepository _repository;
    private readonly IBaseRepository<PacientePlanoMedico> _planoMedicoRepository;

    public DeletePacientePlanoMedicoRequestHandler(IPacienteRepository repository, IBaseRepository<PacientePlanoMedico> planoMedicoRepository)
    {
        _repository = repository;
        _planoMedicoRepository = planoMedicoRepository;
    }

    public async Task<Result<bool>> Handle(DeletePacientePlanoMedicoRequest request, CancellationToken cancellationToken)
    {
        var paciente = await _repository.GetByIdAsync(request.PacienteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        var planoMedico = await _planoMedicoRepository.GetByFilterAsync(c => c.PacienteId == request.PacienteId && c.PlanoMedicoId == request.PlanoMedicoId);
        NotFoundException.ThrowIfNull(planoMedico, $"Plano Medico com o ID: '{request.PlanoMedicoId}' não foi encontrado para o Paciente com o ID: '{request.PacienteId}'");

        planoMedico.ChangeStatus(status: false);

        var rowsAffected = await _planoMedicoRepository.ChangeStatusAsync(planoMedico);

        return await Task.FromResult(rowsAffected > 0);
    }
}