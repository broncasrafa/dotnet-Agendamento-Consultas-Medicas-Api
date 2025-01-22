using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Responses;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteById;

public class SelectPacienteByIdRequestHandler : IRequestHandler<SelectPacienteByIdRequest, Result<PacienteResponse>>
{
    private readonly IPacienteRepository _repository;

    public SelectPacienteByIdRequestHandler(IPacienteRepository repository) => _repository = repository;

    public async Task<Result<PacienteResponse>> Handle(SelectPacienteByIdRequest request, CancellationToken cancellationToken)
    {
        var paciente = await _repository.GetByIdDetailsAsync(request.PacienteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");
        return await Task.FromResult(PacienteResponse.MapFromEntity(paciente));
    }
}