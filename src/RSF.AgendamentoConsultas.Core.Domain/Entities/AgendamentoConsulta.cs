using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using RSF.AgendamentoConsultas.Core.Domain.Validation;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;

namespace RSF.AgendamentoConsultas.Core.Domain.Entities;

public class AgendamentoConsulta
{    
    public int AgendamentoConsultaId { get; set; }
    public int? EspecialistaId { get; set; }
    public int? EspecialidadeId { get; set; }
    public int? LocalAtendimentoId { get; set; }
    public bool? AgendamentoDependente { get; private set; }
    public int? PacienteId { get; set; }
    public int? PlanoMedicoId { get; set; }
    public int? DependenteId { get; set; }
    public int? DependentePlanoMedicoId { get; set; }
    public int TipoConsultaId { get; set; }
    public int TipoAgendamentoId { get; set; }
    public int StatusConsultaId { get; set; }
    public string TelefoneContato { get; set; }
    public string MotivoConsulta { get; set; }
    public DateTime DataConsulta { get; set; }
    /// <summary>
    /// Horário da consulta no formato HH:mm
    /// </summary>
    public string HorarioConsulta { get; set; }
    public decimal? ValorConsulta { get; set; }
    public bool? PrimeiraVez { get; set; }
    public int? DuracaoEmMinutosConsulta { get; set; }
    public string Observacoes { get; set; }
    public string NotaCancelamento { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ConfirmedByPacienteAt { get; set; }
    public DateTime? ConfirmedByEspecialistaAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Especialista Especialista { get; set; }
    public Especialidade Especialidade { get; set; }
    public EspecialistaLocalAtendimento LocalAtendimento { get; set; }    
    public TipoStatusConsulta StatusConsulta { get; set; }
    public TipoConsulta TipoConsulta { get; set; }
    public TipoAgendamento TipoAgendamento { get; set; }
    public Paciente Paciente { get; set; }
    public PacientePlanoMedico PlanoMedico { get; set; }
    public PacienteDependente? Dependente { get; set; }
    public PacienteDependentePlanoMedico? PlanoMedicoDependente { get; set; }

    protected AgendamentoConsulta() { }

    public AgendamentoConsulta(
        int especialistaId, 
        int especialidadeId, 
        int localAtendimentoId, 
        int tipoConsultaId,
        int tipoAgendamentoId,
        DateTime dataConsulta,
        string horarioConsulta,
        string motivoConsulta,
        decimal? valorConsulta,
        string telefoneContato,
        bool? primeiraVez,
        int pacienteId, 
        int? dependenteId, 
        int planoMedicoId
    )
    {
        EspecialistaId = especialistaId;
        EspecialidadeId = especialidadeId;
        LocalAtendimentoId = localAtendimentoId;
        TipoConsultaId = tipoConsultaId;
        TipoAgendamentoId = tipoAgendamentoId;
        DataConsulta = dataConsulta;
        HorarioConsulta = horarioConsulta;
        MotivoConsulta = motivoConsulta;
        ValorConsulta = (tipoAgendamentoId == (int)ETipoAgendamento.Convenio) ? null : valorConsulta;
        TelefoneContato = telefoneContato;
        PrimeiraVez = primeiraVez ?? false;
        PacienteId = pacienteId;
        DependenteId = dependenteId;
        PlanoMedicoId = (dependenteId is null) ? planoMedicoId : null;
        DependentePlanoMedicoId = (dependenteId is not null) ? planoMedicoId : null;
        AgendamentoDependente = dependenteId is not null;
        StatusConsultaId = (int)ETipoStatusConsulta.Solicitado;
        CreatedAt = DateTime.Now;

        Validate();
    }



    public void ConfirmarPaciente()
    {
        if (StatusConsultaId != (int)ETipoStatusConsulta.Solicitado)
            throw new EntityValidationException($"Status da Consulta inválido para confirmação");

        if (DataConsulta <= DateTime.Now)
            throw new EntityValidationException($"Data da Consulta inválido para confirmação");

        if (!ConfirmedByEspecialistaAt.HasValue)
            throw new EntityValidationException($"Agendamento não confirmado pelo especialista");

        if (ConfirmedByEspecialistaAt.Value.AddDays(1) > DateTime.Now)
            throw new EntityValidationException($"Consulta cancelada automaticamente, pois não recebemos sua resposta para a confirmação em tempo hábil.");

        StatusConsultaId = (int)ETipoStatusConsulta.Confirmado;
        ConfirmedByPacienteAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
    public void ConfirmarProfissional()
    {
        if (StatusConsultaId != (int)ETipoStatusConsulta.Solicitado)
            throw new EntityValidationException($"Status da Consulta inválido para confirmação");

        if (DataConsulta <= DateTime.Now)
            throw new EntityValidationException($"Data da Consulta inválido para confirmação");
        
        ConfirmedByEspecialistaAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
    public void Cancelar(string notaCancelamento)
    {
        DomainValidation.NotNullOrEmpty(notaCancelamento, nameof(NotaCancelamento));

        if (StatusConsultaId != (int)ETipoStatusConsulta.Solicitado &&
            StatusConsultaId != (int)ETipoStatusConsulta.Confirmado)
            throw new EntityValidationException($"Status da Consulta inválido para cancelamento");

        if (DataConsulta <= DateTime.Now)
            throw new EntityValidationException($"Data da Consulta inválido para cancelamento");

        NotaCancelamento = notaCancelamento;
        StatusConsultaId = (int)ETipoStatusConsulta.Cancelado;
        UpdatedAt = DateTime.Now;
    }

    public void ExpirarProfissional(string notaCancelamento)
    {
        DomainValidation.NotNullOrEmpty(notaCancelamento, nameof(NotaCancelamento));

        NotaCancelamento = notaCancelamento;
        StatusConsultaId = (int)ETipoStatusConsulta.ExpiradoProfissional;
        UpdatedAt = DateTime.Now;
    }
    public void ExpirarPaciente(string notaCancelamento)
    {
        DomainValidation.NotNullOrEmpty(notaCancelamento, nameof(NotaCancelamento));

        NotaCancelamento = notaCancelamento;
        StatusConsultaId = (int)ETipoStatusConsulta.ExpiradoPaciente;
        UpdatedAt = DateTime.Now;
    }

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(HorarioConsulta, nameof(HorarioConsulta));
        DomainValidation.NotNullOrEmpty(MotivoConsulta, nameof(MotivoConsulta));
        DomainValidation.NotNullOrEmpty(TelefoneContato, nameof(TelefoneContato));

        DomainValidation.IdentifierGreaterThanZero(EspecialistaId, nameof(EspecialistaId));
        DomainValidation.IdentifierGreaterThanZero(EspecialidadeId, nameof(EspecialidadeId));
        DomainValidation.IdentifierGreaterThanZero(LocalAtendimentoId, nameof(LocalAtendimentoId));
        DomainValidation.IdentifierGreaterThanZero(TipoConsultaId, nameof(TipoConsultaId));
        DomainValidation.IdentifierGreaterThanZero(TipoAgendamentoId, nameof(TipoAgendamentoId));
        DomainValidation.IdentifierGreaterThanZero(StatusConsultaId, nameof(StatusConsultaId));
        
        DomainValidation.IdentifierGreaterThanZero(PacienteId, nameof(PacienteId));
        if (PlanoMedicoId is not null)
            DomainValidation.IdentifierGreaterThanZero(PlanoMedicoId, nameof(PlanoMedicoId));
        if (DependenteId is not null)
            DomainValidation.IdentifierGreaterThanZero(DependenteId, nameof(DependenteId));
        if (DependentePlanoMedicoId is not null)
            DomainValidation.IdentifierGreaterThanZero(DependentePlanoMedicoId, nameof(DependentePlanoMedicoId));

        DomainValidation.PossiblesValidTypes(TypeValids.VALID_TIPO_AGENDAMENTOS, value: TipoAgendamentoId, nameof(TipoAgendamentoId));
        DomainValidation.PossiblesValidTypes(TypeValids.VALID_TIPO_CONSULTAS, value: TipoConsultaId, nameof(TipoConsultaId));
        DomainValidation.PossiblesValidTypes(TypeValids.VALID_TIPO_STATUS_CONSULTAS, value: StatusConsultaId, nameof(StatusConsultaId));

        DomainValidation.PossibleValidDate(DataConsulta.ToString("yyyy-MM-dd"), permitirSomenteDatasFuturas: true, nameof(DataConsulta));
        DomainValidation.PossibleValidTime(HorarioConsulta, nameof(HorarioConsulta));

        DomainValidation.PossibleValidPhoneNumber(TelefoneContato, nameof(TelefoneContato));

        if (ValorConsulta is not null)
            DomainValidation.PriceGreaterThanZero(ValorConsulta, nameof(ValorConsulta));
    }
}




