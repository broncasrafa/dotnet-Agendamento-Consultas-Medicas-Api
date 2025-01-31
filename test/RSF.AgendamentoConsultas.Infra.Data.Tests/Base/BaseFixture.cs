using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Infra.Data.Context;
using Bogus;

namespace RSF.AgendamentoConsultas.Infra.Data.Tests.Base;

public class BaseFixture
{
    protected Faker Faker { get; set; }

    public BaseFixture() => Faker = new Faker("pt_BR");

    public static AppDbContext CreateDbContext()
    {
        var context = new AppDbContext(
            new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("DB_AgendamentoConsultasMedicas_Tests")
            .EnableSensitiveDataLogging()
            .Options
        );

        return context;
    }
}