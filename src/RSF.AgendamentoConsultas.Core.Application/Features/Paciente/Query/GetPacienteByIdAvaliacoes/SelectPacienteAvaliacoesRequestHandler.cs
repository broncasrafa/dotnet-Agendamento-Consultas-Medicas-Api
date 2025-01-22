using RSF.AgendamentoConsultas.Core.Domain.Interfaces;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Responses;
using MediatR;
using OperationResult;


namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteByIdAvaliacoes;

public class SelectPacienteAvaliacoesRequestHandler : IRequestHandler<SelectPacienteAvaliacoesRequest, Result<PacienteResultList<PacienteAvaliacaoResponse>>>
{
    private readonly IPacienteRepository _repository;

    public SelectPacienteAvaliacoesRequestHandler(IPacienteRepository pacienteRepository) => _repository = pacienteRepository;

    public async Task<Result<PacienteResultList<PacienteAvaliacaoResponse>>> Handle(SelectPacienteAvaliacoesRequest request, CancellationToken cancellationToken)
    {
        var paciente = await _repository.GetByIdDetailsAsync(request.PacienteId);

        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        var avaliacoes = await _repository.GetAvaliacoesPacienteByIdAsync(request.PacienteId);

        var response = new PacienteResultList<PacienteAvaliacaoResponse>(request.PacienteId, PacienteAvaliacaoResponse.MapFromEntity(avaliacoes));

        return Result.Success(response);
    }
}