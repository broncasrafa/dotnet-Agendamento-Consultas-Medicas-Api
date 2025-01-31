using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.Infra.Data.Tests.Base;


namespace RSF.AgendamentoConsultas.Infra.Data.Tests.Fixtures;

[CollectionDefinition(nameof(AgendamentoConsultaRepositoryTestFixture))]
public class AgendamentoConsultaRepositoryTestFixtureCollection : ICollectionFixture<AgendamentoConsultaRepositoryTestFixture> { }

public class AgendamentoConsultaRepositoryTestFixture : BaseFixture
{
    public ICollection<AgendamentoConsulta> GetAgendamentosExpiradosPaciente()
    {
        return new List<AgendamentoConsulta>
        {
            new(
                especialistaId: 1,
                especialidadeId: 1,
                localAtendimentoId: 1,
                tipoConsultaId: (int)ETipoConsulta.Presencial,
                tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
                dataConsulta: DateTime.Now.AddDays(5),
                horarioConsulta: "12:20",
                motivoConsulta: Faker.Lorem.Text(),
                valorConsulta: null,
                telefoneContato: "1199999565",
                primeiraVez: false,
                pacienteId: 1,
                dependenteId: null,
                planoMedicoId: 1),

            new(
                especialistaId: 1,
                especialidadeId: 1,
                localAtendimentoId: 1,
                tipoConsultaId: (int)ETipoConsulta.Presencial,
                tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
                dataConsulta: DateTime.Now.AddDays(10),
                horarioConsulta: "12:20",
                motivoConsulta: Faker.Lorem.Text(),
                valorConsulta: null,
                telefoneContato: "1199999565",
                primeiraVez: false,
                pacienteId: 2,
                dependenteId: null,
                planoMedicoId: 1),

            new(
                especialistaId: 1,
                especialidadeId: 1,
                localAtendimentoId: 1,
                tipoConsultaId: (int)ETipoConsulta.Presencial,
                tipoAgendamentoId: (int)ETipoAgendamento.Convenio,
                dataConsulta: DateTime.Now.AddDays(10),
                horarioConsulta: "12:20",
                motivoConsulta: Faker.Lorem.Text(),
                valorConsulta: null,
                telefoneContato: "1199999565",
                primeiraVez: false,
                pacienteId: 3,
                dependenteId: null,
                planoMedicoId: 1)            
        };
    }
}