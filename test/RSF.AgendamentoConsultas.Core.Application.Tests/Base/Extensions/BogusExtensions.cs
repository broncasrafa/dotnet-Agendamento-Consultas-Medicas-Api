using System.Text.RegularExpressions;
using Bogus;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Base.Extensions;

public static class BogusExtensions
{
    public static string CustomCellPhoneBR(this Person person)
    {
        var faker = new Faker(locale: "pt_BR");
        var phone = faker.Random.ReplaceNumbers("###########");
        return Regex.Replace(phone, @"\D", "");
    }

    public static string CustomPhoneBR(this Person person)
    {
        var faker = new Faker(locale: "pt_BR");
        var phone = faker.Random.ReplaceNumbers("##########");
        return Regex.Replace(phone, @"\D", "");
    }

    public static string CustomDateOfBirth(this Faker faker, int maxAgeInYears = 10)
    {
        var date = faker.Date.Between(DateTime.Today.AddYears(-maxAgeInYears), DateTime.Today);
        return date.ToString("yyyy-MM-dd");
    }

    public static string CustomGender(this Faker faker)
    {
        string[] generos = { "Masculino", "Feminino" };
        return faker.PickRandom(generos);
    }

    public static decimal CustomHeight(this Faker faker)
    {
        double height = faker.Random.Double(1.20, 2.20);
        return Math.Truncate((decimal)height * 100) / 100;
    }

    public static decimal CustomWeight(this Faker faker)
    {
        double weight = faker.Random.Double(30.00, 200.00);
        return Math.Truncate((decimal)weight * 100) / 100;
    }
}