using RSF.AgendamentoConsultas.Core.Domain.Tests.Base;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using Bogus;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.Fixtures;

[CollectionDefinition(nameof(EspecialistaTestFixture))]
public class EspecialistaTestFixtureCollection : ICollectionFixture<EspecialistaTestFixture> { }

public class EspecialistaTestFixture : BaseFixture
{
    public Especialista GetEntity()
        => new(
            userId: Guid.NewGuid().ToString(),
            nome: Faker.Person.FirstName,
            licenca: Faker.Commerce.ProductName(),
            email: Faker.Internet.Email(),
            genero: Faker.PickRandom(new string[] { "Masculino", "Feminino" }),
            tipo: Faker.PickRandom(new string[] { "Basic", "Premium" }),
            foto: "https://static.vecteezy.com/teste.png",
            agendaOnline: Faker.PickRandomParam(new bool[] { true, false }),
            telemedicinaOnline: Faker.PickRandomParam(new bool[] { true, false }),
            telemedicinaAtivo: Faker.PickRandomParam(new bool[] { true, false }),
            telemedicinaPrecoNumber: Faker.Random.Decimal(100, 2000),
            experienciaProfissional: Faker.Lorem.Text(),
            formacaoAcademica: Faker.Lorem.Text());
}