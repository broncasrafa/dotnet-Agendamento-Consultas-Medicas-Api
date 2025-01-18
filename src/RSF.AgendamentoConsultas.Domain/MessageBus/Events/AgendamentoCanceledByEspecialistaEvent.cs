﻿namespace RSF.AgendamentoConsultas.Domain.MessageBus.Events;

public class AgendamentoCanceledByEspecialistaEvent : Event
{    
    public string PacienteNome { get; private set; }
    public string PacienteEmail { get; private set; }
    public string EspecialistaNome { get; private set; }
    public string Especialidade { get; private set; }
    public string DataConsulta { get; private set; }
    public string HorarioConsulta { get; private set; }
    public string LocalAtendimento { get; private set; }

    public AgendamentoCanceledByEspecialistaEvent(
        string pacienteNome, 
        string pacienteEmail, 
        string especialistaNome, 
        string especialidade, 
        string dataConsulta, 
        string horarioConsulta, 
        string localAtendimento)
    {
        PacienteNome = pacienteNome;
        PacienteEmail = pacienteEmail;
        EspecialistaNome = especialistaNome;
        Especialidade = especialidade;
        DataConsulta = dataConsulta;
        HorarioConsulta = horarioConsulta;
        LocalAtendimento = localAtendimento;
    }
}