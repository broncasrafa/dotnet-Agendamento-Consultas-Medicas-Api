using RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Responses;

public class PacienteResponse
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public bool TelefoneVerificado { get; set; }
    public bool TermoUsoAceito { get; set; }
    public string Genero { get; set; }
    public string DataNascimento { get; set; }
    public DateTime CreatedAt { get; set; }
    public IReadOnlyList<PacienteDependenteResponse>? Dependentes { get; set; }
    public IReadOnlyList<PacientePlanoMedicoResponse>? PlanosMedicos { get; set; }

    public PacienteResponse()
    {
    }

    public static PacienteResponse MapFromEntity(Domain.Entities.Paciente paciente)
        => paciente is null ? default! : new PacienteResponse
        {
            Id = paciente.PacienteId,
            Nome = paciente.Nome,
            Telefone = paciente.Telefone,
            Email = paciente.Email,
            CPF = paciente.CPF,
            TelefoneVerificado = paciente.TelefoneVerificado,
            TermoUsoAceito = paciente.TermoUsoAceito,
            Genero = paciente.Genero,
            DataNascimento = paciente.DataNascimento,
            CreatedAt = paciente.CreatedAt,
            Dependentes = PacienteDependenteResponse.MapFromEntity(paciente.Dependentes),
            PlanosMedicos = PacientePlanoMedicoResponse.MapFromEntity(paciente.PlanosMedicos)
        };
}