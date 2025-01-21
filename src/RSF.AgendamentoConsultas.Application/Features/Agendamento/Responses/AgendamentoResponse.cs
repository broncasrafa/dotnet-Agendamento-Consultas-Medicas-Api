using RSF.AgendamentoConsultas.Application.Features.Especialidade.Responses;
using RSF.AgendamentoConsultas.Application.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Application.Features.Agendamento.Responses;

public class AgendamentoResponse
{
    public int Id { get; set; }
    public bool AgendamentoDependente { get; set; }
    public string TelefoneContato { get; set; }
    public string MotivoConsulta { get; set; }
    public DateTime DataConsulta { get; set; }
    public string HorarioConsulta { get; set; }
    public decimal? ValorConsulta { get; set; }
    public bool PrimeiraVez { get; set; }
    public int? DuracaoEmMinutosConsulta { get; set; }
    public string Observacoes { get; set; }
    public string NotaCancelamento { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ConfirmedByPacienteAt { get; set; }
    public DateTime? ConfirmedByEspecialistaAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string StatusConsulta { get; set; }
    public string TipoConsulta { get; set; }
    public string TipoAgendamento { get; set; }
    public string Paciente { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public string MetodoPagamento { get; set; }
    public string NumeroCarteirinha { get; set; }

    public EspecialistaResponse Profissional { get; set; }
    public EspecialidadeResponse Especialidade { get; set; }
    public EspecialistaLocalAtendimentoResponse LocalAtendimento { get; set; }

    public static AgendamentoResponse MapFromEntity(AgendamentoConsulta entity)
    {
        if (entity is null) return default!;

        return new AgendamentoResponse
        {
            Id = entity.AgendamentoConsultaId,
            AgendamentoDependente = entity.AgendamentoDependente ?? false,
            TelefoneContato = entity.TelefoneContato,
            MotivoConsulta = entity.MotivoConsulta,
            DataConsulta = entity.DataConsulta,
            HorarioConsulta = entity.HorarioConsulta,
            ValorConsulta = entity.ValorConsulta,
            PrimeiraVez = entity.PrimeiraVez ?? false,
            DuracaoEmMinutosConsulta = entity.DuracaoEmMinutosConsulta,
            Observacoes = entity.Observacoes,
            NotaCancelamento = entity.NotaCancelamento,
            CreatedAt = entity.CreatedAt,
            ConfirmedByPacienteAt = entity.ConfirmedByPacienteAt,
            ConfirmedByEspecialistaAt = entity.ConfirmedByEspecialistaAt,
            UpdatedAt = entity.UpdatedAt,
            StatusConsulta = entity.StatusConsulta.Descricao.ToUpper(),
            TipoConsulta = entity.TipoConsulta.Descricao,
            TipoAgendamento = entity.TipoAgendamento.Descricao,
            Paciente = entity.Dependente is null ? entity.Paciente.Nome : entity.Dependente.Nome,
            CPF = entity.Dependente is null ? entity.Paciente.CPF : entity.Dependente.CPF,
            Email = entity.Dependente is null ? entity.Paciente.Email : entity.Dependente.Email,
            MetodoPagamento = entity.Dependente is null ? $"{entity.PlanoMedico.ConvenioMedico.Nome} / {entity.PlanoMedico.NomePlano}" : $"{entity.PlanoMedicoDependente!.ConvenioMedico.Nome} / {entity.PlanoMedicoDependente!.NomePlano}",
            NumeroCarteirinha = entity.Dependente is null ? entity.PlanoMedico.NumCartao : entity.PlanoMedicoDependente!.NumCartao,
            Profissional = EspecialistaResponse.MapFromEntity(entity.Especialista),
            Especialidade = EspecialidadeResponse.MapFromEntity(entity.Especialidade),
            LocalAtendimento = EspecialistaLocalAtendimentoResponse.MapFromEntity(entity.LocalAtendimento)
        };
    }

    public static IReadOnlyList<AgendamentoResponse>? MapFromEntity(IEnumerable<AgendamentoConsulta> collection)
        => collection is null || !collection.Any() ? default! : collection.Select(c => MapFromEntity(c)).ToList();
}
