using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using RSF.AgendamentoConsultas.Core.Domain.Tests.Base.Extensions;
using FluentAssertions;
using Bogus;
using Bogus.Extensions.Brazil;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.Entities;

public class PacienteTest
{
    private readonly Faker _faker;

    public PacienteTest() => _faker = new Faker(locale: "pt_BR");


    [Fact]
    public void Paciente_ShouldBeValid_WhenAddNew_AllFieldsIsValid()
    {
        // Arrange
        var obj = new Paciente(
            userId: Guid.NewGuid().ToString(),
            nome: _faker.Person.FullName,
            cpf: _faker.Person.Cpf(false),
            email: _faker.Internet.Email(),
            telefone: _faker.Person.CustomCellPhoneBR(),
            genero: _faker.CustomGender(),
            dataNascimento: _faker.CustomDateOfBirth(20),
            nomeSocial: _faker.Person.FirstName,
            peso: _faker.CustomWeight(),
            altura: _faker.CustomHeight(),
            telefoneVerificado: _faker.PickRandomParam(new bool[] { true, false }),
            emailVerificado: _faker.PickRandomParam(new bool[] { true, false }),
            termoUsoAceito: _faker.PickRandomParam(new bool[] { true, false })
        );

        // Act & Assert
        FluentActions.Invoking(() => obj).Should().NotThrow();

        obj.Should().NotBeNull();
    }

    [Fact]
    public void Paciente_ShouldBeValid_WhenUpdate_AllFieldsIsValid()
    {
        // Arrange
        var obj = new Paciente(
            userId: Guid.NewGuid().ToString(),
            nome: _faker.Person.FullName,
            cpf: _faker.Person.Cpf(false),
            email: _faker.Internet.Email(),
            telefone: _faker.Person.CustomCellPhoneBR(),
            genero: _faker.CustomGender(),
            dataNascimento: _faker.CustomDateOfBirth(20),
            nomeSocial: _faker.Person.FirstName,
            peso: _faker.CustomWeight(),
            altura: _faker.CustomHeight(),
            telefoneVerificado: _faker.PickRandomParam(new bool[] { true, false }),
            emailVerificado: _faker.PickRandomParam(new bool[] { true, false }),
            termoUsoAceito: _faker.PickRandomParam(new bool[] { true, false })
        );

        // Act & Assert
        FluentActions.Invoking(() => obj.Update(
            nome: _faker.Person.FullName,
            email: _faker.Internet.Email(),
            telefone: _faker.Person.CustomCellPhoneBR(),
            genero: _faker.CustomGender(),
            dataNascimento: _faker.CustomDateOfBirth(20),
            nomeSocial: _faker.Person.FirstName,
            peso: _faker.CustomWeight(),
            altura: _faker.CustomHeight(),
            telefoneVerificado: _faker.PickRandomParam(new bool[] { true, false }),
            emailVerificado: _faker.PickRandomParam(new bool[] { true, false }),
            termoUsoAceito: _faker.PickRandomParam(new bool[] { true, false }))
        ).Should().NotThrow();

        obj.Should().NotBeNull();
        obj.UpdatedAt.Should().NotBeNull();
    }

    [Theory(DisplayName = "Deve alterar o status corretamente ao chamar ChangeStatus")]
    [InlineData(true)]
    [InlineData(false)]
    public void Paciente_ChangeStatus_ShouldChangeStatus_WhenCalledWithBooleanValue(bool status)
    {
        // Arrange
        var obj = new Paciente(
            userId: Guid.NewGuid().ToString(),
            nome: _faker.Person.FullName,
            cpf: _faker.Person.Cpf(false),
            email: _faker.Internet.Email(),
            telefone: _faker.Person.CustomCellPhoneBR(),
            genero: _faker.CustomGender(),
            dataNascimento: _faker.CustomDateOfBirth(20),
            nomeSocial: _faker.Person.FirstName,
            peso: _faker.CustomWeight(),
            altura: _faker.CustomHeight(),
            telefoneVerificado: _faker.PickRandomParam(new bool[] { true, false }),
            emailVerificado: _faker.PickRandomParam(new bool[] { true, false }),
            termoUsoAceito: _faker.PickRandomParam(new bool[] { true, false })
        )
        { Ativo = !status };

        // Act
        obj.ChangeStatus(status);

        // Assert
        obj.Ativo.Should().Be(status);
    }


    [Theory]
    [InlineData(null, "Teste", "44313054596", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "UserId não pode ser nulo ou vazio.")]
    [InlineData("", "Teste", "44313054596", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "UserId não pode ser nulo ou vazio.")]
    [InlineData("userId", null, "44313054596", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "Nome não pode ser nulo ou vazio.")]
    [InlineData("userId", "", "44313054596", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "Nome não pode ser nulo ou vazio.")]
    [InlineData("userId", "Teste", null, "email@teste.com", "11965481203", "Masculino", "1990-11-04", "CPF não pode ser nulo ou vazio.")]
    [InlineData("userId", "Teste", "", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "CPF não pode ser nulo ou vazio.")]
    [InlineData("userId", "Teste", "4431305", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "CPF deve conter exatamente 11 digitos.")]
    [InlineData("userId", "Teste", "4431305ac4", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "CPF deve conter exatamente 11 digitos.")]
    [InlineData("userId", "Teste", "44313050124", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "CPF inválido.")]
    [InlineData("userId", "Teste", "11111111111", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "CPF inválido.")]
    [InlineData("userId", "Teste", "44313054596", null, "11965481203", "Masculino", "1990-11-04", "Email não pode ser nulo ou vazio.")]
    [InlineData("userId", "Teste", "44313054596", "", "11965481203", "Masculino", "1990-11-04", "Email não pode ser nulo ou vazio.")]
    [InlineData("userId", "Teste", "44313054596", "teste@", "11965481203", "Masculino", "1990-11-04", "Email com valor inválido.")]
    [InlineData("userId", "Teste", "44313054596", "email@teste.com", null, "Masculino", "1990-11-04", "Telefone não pode ser nulo ou vazio.")]
    [InlineData("userId", "Teste", "44313054596", "email@teste.com", "", "Masculino", "1990-11-04", "Telefone não pode ser nulo ou vazio.")]
    [InlineData("userId", "Teste", "44313054596", "email@teste.com", "invalid-phone", "Masculino", "1990-11-04", "Telefone não pode ser nulo ou vazio.")]
    [InlineData("userId", "Teste", "44313054596", "email@teste.com", "12345", "Masculino", "1990-11-04", "Telefone deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    [InlineData("userId", "Teste", "44313054596", "email@teste.com", "123456789012", "Masculino", "1990-11-04", "Telefone deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    [InlineData("userId", "Teste", "44313054596", "email@teste.com", "11965481203", null, "1990-11-04", "Genero não pode ser nulo ou vazio.")]
    [InlineData("userId", "Teste", "44313054596", "email@teste.com", "11965481203", "", "1990-11-04", "Genero não pode ser nulo ou vazio.")]
    [InlineData("userId", "Teste", "44313054596", "email@teste.com", "11965481203", "genero", "1990-11-04", "Genero inválido. Valores válidos: 'Masculino, Feminino, Não informado'")]
    [InlineData("userId", "Teste", "44313054596", "email@teste.com", "11965481203", "Masculino", null, "DataNascimento não pode ser nulo ou vazio.")]
    [InlineData("userId", "Teste", "44313054596", "email@teste.com", "11965481203", "Masculino", "", "DataNascimento não pode ser nulo ou vazio.")]
    [InlineData("userId", "Teste", "44313054596", "email@teste.com", "11965481203", "Masculino", "invalid-date", "DataNascimento está em um formato inválido. O formato correto é yyyy-MM-dd.")]
    [InlineData("userId", "Teste", "44313054596", "email@teste.com", "11965481203", "Masculino", "2150-12-31", "DataNascimento deve ser uma data passada e menor que hoje.")]
    public void Paciente_ShouldThrowException_WhenAddNew_FieldsAreInvalids(string userId, string nome, string cpf, string email, string telefone, string genero, string dataNascimento, string expectedMessage)
    {
        // Arrange & Act
        var ex = Assert.Throws<EntityValidationException>(() =>
            new Paciente(
                userId: userId,
                nome: nome,
                cpf: cpf,
                email: email,
                telefone: telefone,
                genero: genero,
                dataNascimento: dataNascimento,
                nomeSocial: _faker.Person.FirstName,
                peso: _faker.CustomWeight(),
                altura: _faker.CustomHeight(),
                telefoneVerificado: _faker.PickRandomParam(new bool[] { true, false }),
                emailVerificado: _faker.PickRandomParam(new bool[] { true, false }),
                termoUsoAceito: _faker.PickRandomParam(new bool[] { true, false }))
            );

        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }


    [Theory]
    [InlineData(null, "email@teste.com", "11965481203", "Masculino", "1990-11-04", "Nome não pode ser nulo ou vazio.")]
    [InlineData("", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "Nome não pode ser nulo ou vazio.")]
    [InlineData("Teste", null, "11965481203", "Masculino", "1990-11-04", "Email não pode ser nulo ou vazio.")]
    [InlineData("Teste", "", "11965481203", "Masculino", "1990-11-04", "Email não pode ser nulo ou vazio.")]
    [InlineData("Teste", "teste@", "11965481203", "Masculino", "1990-11-04", "Email com valor inválido.")]
    [InlineData("Teste", "email@teste.com", null, "Masculino", "1990-11-04", "Telefone não pode ser nulo ou vazio.")]
    [InlineData("Teste", "email@teste.com", "", "Masculino", "1990-11-04", "Telefone não pode ser nulo ou vazio.")]
    [InlineData("Teste", "email@teste.com", "invalid-phone", "Masculino", "1990-11-04", "Telefone não pode ser nulo ou vazio.")]
    [InlineData("Teste", "email@teste.com", "12345", "Masculino", "1990-11-04", "Telefone deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    [InlineData("Teste", "email@teste.com", "123456789012", "Masculino", "1990-11-04", "Telefone deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    [InlineData("Teste", "email@teste.com", "11965481203", null, "1990-11-04", "Genero não pode ser nulo ou vazio.")]
    [InlineData("Teste", "email@teste.com", "11965481203", "", "1990-11-04", "Genero não pode ser nulo ou vazio.")]
    [InlineData("Teste", "email@teste.com", "11965481203", "genero", "1990-11-04", "Genero inválido. Valores válidos: 'Masculino, Feminino, Não informado'")]
    [InlineData("Teste", "email@teste.com", "11965481203", "Masculino", null, "DataNascimento não pode ser nulo ou vazio.")]
    [InlineData("Teste", "email@teste.com", "11965481203", "Masculino", "", "DataNascimento não pode ser nulo ou vazio.")]
    [InlineData("Teste", "email@teste.com", "11965481203", "Masculino", "invalid-date", "DataNascimento está em um formato inválido. O formato correto é yyyy-MM-dd.")]
    [InlineData("Teste", "email@teste.com", "11965481203", "Masculino", "2150-12-31", "DataNascimento deve ser uma data passada e menor que hoje.")]
    public void Paciente_ShouldThrowException_WhenUpdate_FieldsAreInvalids(string nome, string email, string telefone, string genero, string dataNascimento, string expectedMessage)
    {
        // Arrange
        var obj = new Paciente(
            userId: Guid.NewGuid().ToString(),
            nome: _faker.Person.FullName,
            cpf: _faker.Person.Cpf(false),
            email: _faker.Internet.Email(),
            telefone: _faker.Person.CustomCellPhoneBR(),
            genero: _faker.CustomGender(),
            dataNascimento: _faker.CustomDateOfBirth(20),
            nomeSocial: _faker.Person.FirstName,
            peso: _faker.CustomWeight(),
            altura: _faker.CustomHeight(),
            telefoneVerificado: _faker.PickRandomParam(new bool[] { true, false }),
            emailVerificado: _faker.PickRandomParam(new bool[] { true, false }),
            termoUsoAceito: _faker.PickRandomParam(new bool[] { true, false })
        );

        // Arrange & Act
        var ex = Assert.Throws<EntityValidationException>(() =>
            obj.Update(
                nome: nome,
                email: email,
                telefone: telefone,
                genero: genero,
                dataNascimento: dataNascimento)
        );

        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }
}