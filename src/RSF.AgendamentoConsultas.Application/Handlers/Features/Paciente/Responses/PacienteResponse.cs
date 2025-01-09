using RSF.AgendamentoConsultas.Application.Handlers.Features.PacienteDependente.Responses;

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
    DateTime CreatedAt,
    IReadOnlyList<PacienteDependenteResponse>? Dependentes,
    IReadOnlyList<PacientePlanoMedicoResponse>? PlanosMedicos
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
            paciente.CreatedAt,
            PacienteDependenteResponse.MapFromEntity(paciente.Dependentes),
            PacientePlanoMedicoResponse.MapFromEntity(paciente.PlanosMedicos));
}