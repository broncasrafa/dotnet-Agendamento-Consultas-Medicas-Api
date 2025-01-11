using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Helpers;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Command.CreateDependente;

public class CreatePacienteDependenteRequestHandler : IRequestHandler<CreatePacienteDependenteRequest, Result<PacienteDependenteResponse>>
{
    private readonly IPacienteDependenteRepository _repository;

    public CreatePacienteDependenteRequestHandler(IPacienteDependenteRepository repository) => _repository = repository;


    public async Task<Result<PacienteDependenteResponse>> Handle(CreatePacienteDependenteRequest request, CancellationToken cancellationToken)
    {
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
        await _repository.SaveChangesAsync();

        return await Task.FromResult(PacienteDependenteResponse.MapFromEntity(paciente));
    }
}