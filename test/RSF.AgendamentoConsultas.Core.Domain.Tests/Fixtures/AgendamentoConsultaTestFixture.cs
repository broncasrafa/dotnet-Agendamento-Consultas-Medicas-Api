using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Tests.Base;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.Fixtures;

[CollectionDefinition(nameof(AgendamentoConsultaTestFixture))]
public class AgendamentoConsultaTestFixtureCollection : ICollectionFixture<AgendamentoConsultaTestFixture> { }


public class AgendamentoConsultaTestFixture : BaseFixture
{
    public AgendamentoConsulta GetAgendamentoConsulta()
        => new (
            especialistaId: 1,
            especialidadeId: 1,
            localAtendimentoId: 1,
            tipoConsultaId: (int)ETipoConsulta.Presencial,
            tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
            dataConsulta: DateTime.Now.AddDays(5),
            horarioConsulta: "15:30",
            motivoConsulta: "Consulta de rotina",
            valorConsulta: 158.90m,
            telefoneContato: "11965014874",
            primeiraVez: false,
            pacienteId: 1,
            dependenteId: null,
            planoMedicoId: 1);
}