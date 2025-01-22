using RSF.AgendamentoConsultas.Core.Application.Services.HasherPassword;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;


namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.CreatePaciente;

public class CreatePacienteRequestHandler : IRequestHandler<CreatePacienteRequest, Result<PacienteResponse>>
{
    private readonly IPacienteRepository _repository;
    private readonly IPasswordHasher _passwordHasher;

    public CreatePacienteRequestHandler(IPacienteRepository repository, IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<PacienteResponse>> Handle(CreatePacienteRequest request, CancellationToken cancellationToken)
    {
        var paciente = await _repository.GetByFilterAsync(c => c.CPF == request.CPF);
        AlreadyExistsException.ThrowIfExists(paciente, $"Paciente com o CPF: '{request.CPF}' já cadastrado");

        paciente = await _repository.GetByFilterAsync(c => c.Email == request.Email);
        AlreadyExistsException.ThrowIfExists(paciente, $"Paciente com o e-mail: '{request.Email}' já cadastrado");

        var newPaciente = new Domain.Entities.Paciente(
            nome: request.NomeCompleto,
            cpf: request.CPF,
            email: request.Email,
            telefone: request.Telefone,
            genero: request.Genero,
            dataNascimento: request.DataNascimento.ToString("yyyy-MM-dd"),
            termoUsoAceito: request.TermoUsoAceito);

        // [TODO]: salvar a senha, idPacientePrincipal, email, cpf, telefone na tabela de usuarios (talvez um Identity)
        newPaciente.SetPassword(_passwordHasher.Hash(request.Password));

        await _repository.AddAsync(newPaciente);

        return await Task.FromResult(PacienteResponse.MapFromEntity(newPaciente));
    }
}