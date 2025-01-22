namespace RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;

public class AgendamentoExpiredByEspecialistaEvent : Event
{    
    public string PacienteNome { get; private set; }
    public string EspecialistaNome { get; private set; }
    public string EspecialistaEmail { get; private set; }
    public string Especialidade { get; private set; }
    public string DataConsulta { get; private set; }
    public string HorarioConsulta { get; private set; }
    public string LocalAtendimento { get; private set; }

    public AgendamentoExpiredByEspecialistaEvent(
        string pacienteNome, 
        string especialistaNome, 
        string especialistaEmail, 
        string especialidade, 
        string dataConsulta, 
        string horarioConsulta, 
        string localAtendimento)
    {
        PacienteNome = pacienteNome;
        EspecialistaNome = especialistaNome;
        EspecialistaEmail = especialistaEmail;
        Especialidade = especialidade;
        DataConsulta = dataConsulta;
        HorarioConsulta = horarioConsulta;
        LocalAtendimento = localAtendimento;
    }
}