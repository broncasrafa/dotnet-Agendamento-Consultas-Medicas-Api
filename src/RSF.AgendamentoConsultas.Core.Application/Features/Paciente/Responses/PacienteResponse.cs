using RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Responses;

public class PacienteResponse
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public bool? TelefoneVerificado { get; set; }
    public bool? TermoUsoAceito { get; set; }
    public string Genero { get; set; }
    public string DataNascimento { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? Ativo { get; set; }
    public IReadOnlyList<PacienteDependenteResponse>? Dependentes { get; set; }
    public IReadOnlyList<PacientePlanoMedicoResponse>? PlanosMedicos { get; set; }
    

    public PacienteResponse()
    {
    }

    public static PacienteResponse MapFromEntity(Domain.Entities.Paciente entity)
        => entity is null ? default! : new PacienteResponse
        {
            Id = entity.PacienteId,
            Nome = entity.Nome,
            Telefone = entity.Telefone,
            Email = entity.Email,
            CPF = entity.CPF,
            TelefoneVerificado = entity.TelefoneVerificado,
            TermoUsoAceito = entity.TermoUsoAceito,
            Genero = entity.Genero,
            DataNascimento = entity.DataNascimento,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Ativo = entity.IsActive,
            Dependentes = PacienteDependenteResponse.MapFromEntity(entity.Dependentes),
            PlanosMedicos = PacientePlanoMedicoResponse.MapFromEntity(entity.PlanosMedicos)
        };
}

public class PacienteLikeDislikeResponse
{
    public int Pacienteid { get; set; }
    public string Nome { get; set; }
    public string Reacao { get; set; }
}