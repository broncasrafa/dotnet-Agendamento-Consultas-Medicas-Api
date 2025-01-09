using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Query.GetPacienteById;

public class SelectPacienteByIdRequestHandler : IRequestHandler<SelectPacienteByIdRequest, Result<PacienteResponse>>
{
    private readonly IPacienteRepository _repository;

    public SelectPacienteByIdRequestHandler(IPacienteRepository repository) => _repository = repository;

    public async Task<Result<PacienteResponse>> Handle(SelectPacienteByIdRequest request, CancellationToken cancellationToken)
    {
        var paciente = await _repository.GetByIdDetailsAsync(request.Id);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.Id}' não foi encontrado");
        return await Task.FromResult(PacienteResponse.MapFromEntity(paciente));
    }
}