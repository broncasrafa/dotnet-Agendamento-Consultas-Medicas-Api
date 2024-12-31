using RSF.AgendamentoConsultas.Domain.Validation;

namespace RSF.AgendamentoConsultas.Domain.Entities;

public class AgendamentoConsulta
{    
    public int AgendamentoConsultaId { get; set; }
    public int EspecialistaId { get; set; }
    public int EspecialidadeId { get; set; }
    public int LocalAtendimentoId { get; set; }
    public int PacienteId { get; set; }
    public int PlanoMedicoId { get; set; }
    public int StatusConsultaId { get; set; }

    public DateTime DataConsulta { get; set; }

    /// <summary>
    /// Horário da consulta no formato HH:mm
    /// </summary>
    public string HorarioConsulta { get; set; }
    public bool PrimeiraVez { get; set; }
    public int? DuracaoEmMinutosConsulta { get; set; }
    public string Observacoes { get; set; }
    public string NotaCancelamento { get; set; }
    public DateTime? ConfirmedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Especialista Especialista { get; set; }
    public Especialidade Especialidade { get; set; }
    public EspecialistaLocalAtendimento LocalAtendimento { get; set; }
    public Paciente Paciente { get; set; }
    public PacientePlanoMedico PlanoMedico { get; set; }
    public TipoStatusConsulta StatusConsulta { get; set; }



    public AgendamentoConsulta(int especialistaId, int especialidadeId, int localAtendimentoId, int pacienteId, int planoMedicoId, int statusConsultaId, DateTime dataConsulta, string horarioConsulta, bool primeiraVez)
    {
        EspecialistaId = especialistaId;
        EspecialidadeId = especialidadeId;
        LocalAtendimentoId = localAtendimentoId;
        PacienteId = pacienteId;
        PlanoMedicoId = planoMedicoId;
        StatusConsultaId = statusConsultaId;
        DataConsulta = dataConsulta;
        HorarioConsulta = horarioConsulta;
        PrimeiraVez = primeiraVez;
        CreatedAt = DateTime.UtcNow;

        DomainValidation.IdentifierGreaterThanZero(EspecialistaId, nameof(EspecialistaId));
        DomainValidation.IdentifierGreaterThanZero(EspecialidadeId, nameof(EspecialidadeId));
        DomainValidation.IdentifierGreaterThanZero(LocalAtendimentoId, nameof(LocalAtendimentoId));
        DomainValidation.IdentifierGreaterThanZero(PacienteId, nameof(PacienteId));
        DomainValidation.IdentifierGreaterThanZero(PlanoMedicoId, nameof(PlanoMedicoId));
        DomainValidation.IdentifierGreaterThanZero(StatusConsultaId, nameof(StatusConsultaId));
        DomainValidation.PossibleValidDate(DataConsulta.ToString("yyyy-MM-dd"), permitirSomenteDatasFuturas: true, nameof(DataConsulta));
        DomainValidation.PossibleValidTime(HorarioConsulta, nameof(HorarioConsulta));
    }
}




