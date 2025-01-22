using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Common;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.UpdatePacientePlanoMedico;

public class UpdatePacientePlanoMedicoRequestHandler : IRequestHandler<UpdatePacientePlanoMedicoRequest, Result<bool>>
{
    private readonly IPacienteRepository _repository;
    private readonly IBaseRepository<PacientePlanoMedico> _planoMedicoRepository;

    public UpdatePacientePlanoMedicoRequestHandler(IPacienteRepository repository, IBaseRepository<PacientePlanoMedico> planoMedicoRepository)
    {
        _repository = repository;
        _planoMedicoRepository = planoMedicoRepository;
    }

    public async Task<Result<bool>> Handle(UpdatePacientePlanoMedicoRequest request, CancellationToken cancellationToken)
    {
        var paciente = await _repository.GetByIdAsync(request.PacienteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        var planoMedico = await _planoMedicoRepository.GetByFilterAsync(c => c.PacienteId == request.PacienteId && c.PlanoMedicoId == request.PlanoMedicoId);
        NotFoundException.ThrowIfNull(planoMedico, $"Plano Medico com o ID: '{request.PlanoMedicoId}' não foi encontrado para o Paciente com o ID: '{request.PacienteId}'");

        planoMedico!.Update(request.NomePlano, request.NumeroCarteirinha);

        var rowsAffected = await _planoMedicoRepository.UpdateAsync(planoMedico);

        return await Task.FromResult(rowsAffected > 0);
    }
}