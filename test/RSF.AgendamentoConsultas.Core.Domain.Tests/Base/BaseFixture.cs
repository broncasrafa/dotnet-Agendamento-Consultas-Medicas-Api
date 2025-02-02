using Bogus;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.Base;

public class BaseFixture
{
    public Faker Faker { get; }

    public BaseFixture() => Faker = new Faker("pt_BR");
}