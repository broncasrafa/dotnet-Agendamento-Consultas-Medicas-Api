using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Domain.MessageBus.Events;

public class AgendamentoConsultaCreatedEvent : Event
{    
    public int AgendamentoConsultaId { get; private set; }
    public string DataAgendamento { get; private set; }
    public string TipoAgendamento { get; private set; }
    public string TipoConsulta { get; private set; }
    public string Especialidade { get; private set; }
    public string Especialista { get; private set; }
    public string EspecialistaEmail { get; private set; }
    public string Paciente { get; private set; }
    public string ConvenioMedico { get; private set; }
    public string MotivoConsulta { get; private set; }
    public string DataConsulta { get; private set; }
    public string HorarioConsulta { get; private set; }
    public bool PrimeiraVez { get; private set; }
    public string LocalAtendimentoNome { get; private set; }
    public string LocalAtendimentoLogradouro { get; private set; }
    public string LocalAtendimentoComplemento { get; private set; }
    public string LocalAtendimentoBairro { get; private set; }
    public string LocalAtendimentoCep { get; private set; }
    public string LocalAtendimentoCidade { get; private set; }
    public string LocalAtendimentoEstado { get; private set; }

    public AgendamentoConsultaCreatedEvent(
        int agendamentoConsultaId,
        string dataAgendamento,
        string tipoAgendamento,
        string tipoConsulta,
        string especialidade,
        string especialista,
        string especialistaEmail,
        string paciente,
        string convenioMedico,
        string motivoConsulta,
        string dataConsulta,
        string horarioConsulta,
        bool primeiraVez,
        string localAtendimentoNome,
        string localAtendimentoLogradouro,
        string localAtendimentoComplemento,
        string localAtendimentoBairro,
        string localAtendimentoCep,
        string localAtendimentoCidade,
        string localAtendimentoEstado)
    {
        AgendamentoConsultaId = agendamentoConsultaId;
        DataAgendamento = dataAgendamento;
        TipoAgendamento = tipoAgendamento;
        TipoConsulta = tipoConsulta;
        Especialidade = especialidade;
        Especialista = especialista;
        EspecialistaEmail = especialistaEmail;
        Paciente = paciente;
        ConvenioMedico = convenioMedico;
        MotivoConsulta = motivoConsulta;
        DataConsulta = dataConsulta;
        HorarioConsulta = horarioConsulta;
        PrimeiraVez = primeiraVez;
        LocalAtendimentoNome = localAtendimentoNome;
        LocalAtendimentoLogradouro = localAtendimentoLogradouro;
        LocalAtendimentoComplemento = localAtendimentoComplemento;
        LocalAtendimentoBairro = localAtendimentoBairro;
        LocalAtendimentoCep = localAtendimentoCep;
        LocalAtendimentoCidade = localAtendimentoCidade;
        LocalAtendimentoEstado = localAtendimentoEstado;
    }
}