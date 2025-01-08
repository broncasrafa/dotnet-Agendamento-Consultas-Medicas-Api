namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Responses;

public record PacienteResponse
(
    int Id,
    string Nome,
    string Telefone,
    string Email,
    string CPF,
    bool TelefoneVerificado,
    bool TermoUsoAceito,
    string Genero,
    string DataNascimento,
    DateTime CreatedAt
)
{
    public static PacienteResponse MapFromEntity(Domain.Entities.Paciente paciente)
        => paciente is null ? default! : new PacienteResponse(
            paciente.PacienteId,
            paciente.Nome,
            paciente.Telefone,
            paciente.Email,
            paciente.CPF,
            paciente.TelefoneVerificado,
            paciente.TermoUsoAceito, 
            paciente.Genero,
            paciente.DataNascimento,
            paciente.CreatedAt);
}