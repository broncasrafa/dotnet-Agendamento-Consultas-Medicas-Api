using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Application.Tests.Base.Extensions;
using Bogus;
using Bogus.Extensions.Brazil;
using Bogus.Extensions.Belgium;
using Moq;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Base;

[ExcludeFromCodeCoverage]
public class BaseFixture
{
    public Faker Faker { get; }
    public Mock<IHttpContextAccessor> HttpContextAccessorMockPaciente { get; }
    public Mock<IHttpContextAccessor> HttpContextAccessorMockEspecialista { get; }
    public Mock<IHttpContextAccessor> HttpContextAccessorMockAdmin { get; }
    public Mock<IConfiguration> ConfigurationMock { get; }

    public string UserId { get; } = "65975d3f-cf29-41d4-8b28-b7172a855149";

    public ApplicationUser UserPaciente { get; set; }
    public ApplicationUser UserEspecialista { get; set; }
    public ApplicationUser UserAdmin { get; set; }

    public Paciente Paciente { get; set; }
    public Especialista Especialista { get; set; }

    public BaseFixture()
    {
        Faker = new Faker("pt_BR");

        HttpContextAccessorMockPaciente = CreateMockHttpContextAccessor(ETipoPerfilAcesso.Paciente, "Paciente");
        HttpContextAccessorMockEspecialista = CreateMockHttpContextAccessor(ETipoPerfilAcesso.Profissional, "Profissional");
        HttpContextAccessorMockAdmin = CreateMockHttpContextAccessor(ETipoPerfilAcesso.Administrador, "Admin");

        UserPaciente = GetUserPaciente();
        UserEspecialista = GetUserEspecialista();
        UserAdmin = GetUserAdministrador();
        Paciente = GetPaciente();
        Especialista = GetEspecialista();

        ConfigurationMock = new Mock<IConfiguration>();
    }

    public void MockRabbitMqQueueConfiguration(string sectionName, string queueName)
    {
        var mockSection = new Mock<IConfigurationSection>();
        mockSection.Setup(c => c.Value).Returns(queueName);
        ConfigurationMock.Setup(c => c.GetSection(sectionName)).Returns(mockSection.Object);
    }

    private Mock<IHttpContextAccessor> CreateMockHttpContextAccessor(ETipoPerfilAcesso perfil, string tipoUsuario)
    {
        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "test-user-id"),
            new Claim(JwtRegisteredClaimNames.Sub, $"TestUser{tipoUsuario}"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, $"TestUser{tipoUsuario}@teste.com"),
            new Claim(ClaimTypes.Name, $"TestUser{tipoUsuario} da Silva"),
            new Claim("uid", UserId),
            new Claim("id", "1"),
            new Claim(ClaimTypes.Role, perfil.ToString())
        };

        var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "mock"));
        var httpContext = new DefaultHttpContext { User = userPrincipal };

        mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

        return mockHttpContextAccessor;
    }

    private ApplicationUser GetUserPaciente() => 
        new ApplicationUser(
            nomeCompleto: "TestUserPaciente da Silva",
            username: $"TestUserPaciente",
            documento: Faker.Person.Cpf(),
            email: $"TestUserPaciente@teste.com",
            genero: Faker.CustomGender(),
            telefone: Faker.Person.CustomCellPhoneBR()) { Id = UserId, IsActive = true };

    private ApplicationUser GetUserEspecialista() =>
        new ApplicationUser(
            nomeCompleto: "TestUserEspecialista da Silva",
            username: $"TestUserEspecialista",
            documento: Faker.Person.NationalNumber(),
            email: $"TestUserEspecialista@teste.com",
            genero: Faker.CustomGender(),
            telefone: Faker.Person.CustomCellPhoneBR()) { Id = UserId, IsActive = true };

    private ApplicationUser GetUserAdministrador() =>
        new ApplicationUser(
            nomeCompleto: "TestUserAdministrador da Silva",
            username: $"TestUserAdministrador",
            documento: Faker.Person.Cpf(),
            email: $"TestUserAdministrador@teste.com",
            genero: Faker.CustomGender(),
            telefone: Faker.Person.CustomCellPhoneBR()) { Id = UserId, IsActive = true };

    private Paciente GetPaciente()
        => new Paciente(
            userId: UserId,
            nome: UserPaciente.NomeCompleto,
            cpf: UserPaciente.Documento,
            email: UserPaciente.Email,
            telefone: UserPaciente.PhoneNumber,
            genero: UserPaciente.Genero,
            dataNascimento: Faker.CustomDateOfBirth()) { PacienteId = 1 };

    private Especialista GetEspecialista()
        => new Especialista(
            userId: UserEspecialista.Id,
            nome: UserEspecialista.NomeCompleto,
            licenca: UserEspecialista.Documento,
            email: UserEspecialista.Email,
            genero: UserEspecialista.Genero,
            tipo: "Premium") { EspecialistaId = 1 };
}