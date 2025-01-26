using RSF.AgendamentoConsultas.Core.Domain.Entities;

namespace RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events
{
    public class DesativarContaCreatedEvent : Event
    {
        public Paciente? Paciente { get; set; }
        public Especialista? Especialista { get; set; }

        public DesativarContaCreatedEvent()
        {
        }
    }
}