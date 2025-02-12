using RSF.AgendamentoConsultas.Core.Application.Tests.Base;
using RSF.AgendamentoConsultas.Core.Application.Tests.Base.Extensions;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using Bogus.Extensions.Brazil;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Agendamento.Fixture;


[CollectionDefinition(nameof(AgendamentoConsultaTestFixture))]
public class AgendamentoConsultaTestFixtureCollection : ICollectionFixture<AgendamentoConsultaTestFixture> { }

public class AgendamentoConsultaTestFixture : BaseFixture
{
    public AgendamentoConsulta GetAgendamentoConsulta()
    {
        var agendamento = new AgendamentoConsulta
        {
            AgendamentoConsultaId = 1,
            EspecialistaId = 1,
            EspecialidadeId = 1,
            LocalAtendimentoId = 1,
            AgendamentoDependente = false,
            PacienteId = 1,
            PlanoMedicoId = 1,
            DependenteId = null,
            DependentePlanoMedicoId = null,
            TipoConsultaId = (int)ETipoConsulta.Presencial,
            TipoAgendamentoId = (int)ETipoAgendamento.Convenio,
            StatusConsultaId = 1,
            TelefoneContato = "11965014874",
            MotivoConsulta = Faker.Lorem.Text(),
            DataConsulta = DateTime.Now.AddDays(5),
            HorarioConsulta = "15:30",
            ValorConsulta = null,
            PrimeiraVez = false,
            DuracaoEmMinutosConsulta = null,
            Observacoes = null,
            NotaCancelamento = null,
            CreatedAt = DateTime.Now,
            Especialista = new Especialista(1, Guid.NewGuid().ToString(),  Faker.Person.FullName, "CRM 123456 SP", Faker.Person.Email, "Masculino"),
            Especialidade = new Especialidade(1, "Teste", "Testes", 1),
            LocalAtendimento = new EspecialistaLocalAtendimento(1, "Clinica Medica", "Rua dos Testes", "Complemento", "Bairro", "04687000", "Cidade", "SP", null, "Tipo", "1156897451", "11965451200"),
            StatusConsulta = new TipoStatusConsulta() { Id = 1, Descricao = "Teste" },
            TipoConsulta = new TipoConsulta { Id = 1, Descricao = "Teste"},
            TipoAgendamento = new TipoAgendamento { Id = 1, Descricao = "Teste" },
            Paciente = new Paciente(1, Guid.NewGuid().ToString(), Faker.Person.FullName, Faker.Person.Cpf(), Faker.Person.Email, Faker.Person.CustomCellPhoneBR(), Faker.CustomGender(), Faker.CustomDateOfBirth()),
            PlanoMedico = new PacientePlanoMedico(1, "Teste", "123456", 1, 1) { ConvenioMedico = new ConvenioMedico(1, "Teste")},
            Dependente = null,
            PlanoMedicoDependente = null
        };


        return agendamento;
    }
}