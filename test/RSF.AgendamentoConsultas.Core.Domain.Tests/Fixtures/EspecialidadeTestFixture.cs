using RSF.AgendamentoConsultas.Core.Domain.Tests.Base;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using Bogus;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.Fixtures;

[CollectionDefinition(nameof(EspecialidadeTestFixture))]
public class EspecialidadeTestFixtureCollection : ICollectionFixture<EspecialidadeTestFixture> { }

public class EspecialidadeTestFixture : BaseFixture
{
    public Especialidade GetEspecialidade()
        => new Especialidade(
            nome: Faker.Commerce.ProductName(),
            nomePlural: Faker.Commerce.ProductName(),
            especialidadeGrupoId: 1);
}