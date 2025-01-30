using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.UpdatePaciente;

public class UpdatePacienteRequestHandler : IRequestHandler<UpdatePacienteRequest, Result<bool>>
{
    private readonly IPacienteRepository _repository;
    private readonly IHttpContextAccessor _httpContext;

    public UpdatePacienteRequestHandler(IPacienteRepository repository, IHttpContextAccessor httpContext)
    {
        _repository = repository;
        _httpContext = httpContext;
    }

    public async Task<Result<bool>> Handle(UpdatePacienteRequest request, CancellationToken cancellationToken)
    {
        HttpContextExtensions.ValidatePermissions(_httpContext.HttpContext, request.PacienteId, ETipoPerfilAcesso.Paciente);

        var paciente = await _repository.GetByIdAsync(request.PacienteId);

        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        paciente.Update(request.NomeCompleto, request.Email, request.Telefone, request.Genero, request.DataNascimento.ToString("yyyy-MM-dd"));

        var rowsAffected = await _repository.UpdateAsync(paciente);

        return await Task.FromResult(rowsAffected > 0);
    }
}