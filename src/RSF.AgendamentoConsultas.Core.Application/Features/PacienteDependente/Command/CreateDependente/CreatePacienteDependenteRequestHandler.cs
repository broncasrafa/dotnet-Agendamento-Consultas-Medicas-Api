using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Helpers;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;
using RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Command.CreateDependente;

public class CreatePacienteDependenteRequestHandler : IRequestHandler<CreatePacienteDependenteRequest, Result<PacienteDependenteResponse>>
{
    private readonly IPacienteDependenteRepository _repository;
    private readonly IHttpContextAccessor _httpContext;

    public CreatePacienteDependenteRequestHandler(IPacienteDependenteRepository repository, IHttpContextAccessor httpContext)
    {
        _repository = repository;
        _httpContext = httpContext;
    }

    public async Task<Result<PacienteDependenteResponse>> Handle(CreatePacienteDependenteRequest request, CancellationToken cancellationToken)
    {
        HttpContextExtensions.ValidatePermissions(_httpContext.HttpContext, request.PacientePrincipalId, ETipoPerfilAcesso.Paciente);

        NotFoundException.ThrowIfNull(request.PacientePrincipalId, $"Paciente Principal com o ID: '{request.PacientePrincipalId}' não foi encontrado");

        var dependente = await _repository.GetByFilterAsync(c => c.CPF == request.CPF && c.PacientePrincipalId == request.PacientePrincipalId);
        AlreadyExistsException.ThrowIfExists(dependente, $"Dependente com o CPF: '{request.CPF}' já cadastrado");

        dependente = await _repository.GetByFilterAsync(c => c.Email == request.Email && c.PacientePrincipalId == request.PacientePrincipalId);
        AlreadyExistsException.ThrowIfExists(dependente, $"Dependente com o e-mail: '{request.Email}' já cadastrado");

        var paciente = new Domain.Entities.PacienteDependente(
            pacientePrincipalId: request.PacientePrincipalId,
            nome: request.NomeCompleto,
            cpf: request.CPF.RemoverFormatacaoSomenteNumeros(),
            email: request.Email,
            telefone: request.Telefone.RemoverFormatacaoSomenteNumeros(),
            genero: request.Genero,
            dataNascimento: request.DataNascimento.ToString("yyyy-MM-dd"));

        await _repository.AddAsync(paciente);

        return await Task.FromResult(PacienteDependenteResponse.MapFromEntity(paciente));
    }
}