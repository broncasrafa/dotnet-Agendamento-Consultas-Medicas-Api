using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using RSF.AgendamentoConsultas.Core.Domain.Tests.Base.Extensions;
using FluentAssertions;
using Bogus;
using Bogus.Extensions.Brazil;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.Entities;

public class PacienteDependenteTest
{
    private readonly Faker _faker;

    public PacienteDependenteTest() => _faker = new Faker(locale: "pt_BR");


    [Fact]
    public void PacienteDependente_ShouldBeValid_WhenAddNew_AllFieldsIsValid()
    {
        // Arrange
        var obj = new PacienteDependente(
            pacientePrincipalId: 1,
            nome: _faker.Person.FullName,
            cpf: _faker.Person.Cpf(false),
            email: _faker.Internet.Email(),
            telefone: _faker.Person.CustomCellPhoneBR(),
            genero: _faker.CustomGender(),
            dataNascimento: _faker.CustomDateOfBirth(20),
            nomeSocial: _faker.Person.FirstName,
            peso: _faker.CustomWeight(),
            altura: _faker.CustomHeight()
        );

        // Act & Assert
        FluentActions.Invoking(() => obj).Should().NotThrow();

        obj.Should().NotBeNull();
    }

    [Fact]
    public void PacienteDependente_ShouldBeValid_WhenUpdate_AllFieldsIsValid()
    {
        // Arrange
        var obj = new PacienteDependente(
            pacientePrincipalId: 1,
            nome: _faker.Person.FullName,
            cpf: _faker.Person.Cpf(false),
            email: _faker.Internet.Email(),
            telefone: _faker.Person.CustomCellPhoneBR(),
            genero: _faker.CustomGender(),
            dataNascimento: _faker.CustomDateOfBirth(20),
            nomeSocial: _faker.Person.FirstName,
            peso: _faker.CustomWeight(),
            altura: _faker.CustomHeight()
        );

        // Act
        FluentActions.Invoking(() => obj.Update(
            nome: _faker.Person.FullName,
            cpf: _faker.Person.Cpf(false),
            email: _faker.Internet.Email(),
            telefone: _faker.Person.CustomCellPhoneBR(),
            genero: _faker.CustomGender(),
            dataNascimento: _faker.CustomDateOfBirth(20),
            nomeSocial: _faker.Person.FirstName,
            peso: _faker.CustomWeight(),
            altura: _faker.CustomHeight())).Should().NotThrow();

        // Assert
        obj.Should().NotBeNull();
        obj.UpdatedAt.Should().NotBeNull();
        obj.Paciente.Should().BeNull();
        obj.PlanosMedicos.Should().BeNull();
        obj.AgendamentosRealizados.Should().BeNull();
    }


    [Theory(DisplayName = "Deve alterar o status corretamente ao chamar ChangeStatus")]
    [InlineData(true)]
    [InlineData(false)]
    public void PacienteDependente_ChangeStatus_ShouldChangeStatus_WhenCalledWithBooleanValue(bool status)
    {
        // Arrange
        var obj = new PacienteDependente(
            pacientePrincipalId: 1,
            nome: _faker.Person.FullName,
            cpf: _faker.Person.Cpf(false),
            email: _faker.Internet.Email(),
            telefone: _faker.Person.CustomCellPhoneBR(),
            genero: _faker.CustomGender(),
            dataNascimento: _faker.CustomDateOfBirth(20),
            nomeSocial: _faker.Person.FirstName,
            peso: _faker.CustomWeight(),
            altura: _faker.CustomHeight()
        ) { Ativo = !status };

        // Act
        obj.ChangeStatus(status);

        // Assert
        obj.Ativo.Should().Be(status);
        obj.Paciente.Should().BeNull();
        obj.PlanosMedicos.Should().BeNull();
        obj.AgendamentosRealizados.Should().BeNull();
    }

    [Theory]
    [InlineData(0, "Teste", "44313054596", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "PacientePrincipalId deve ser maior que zero.")]
    [InlineData(1, null, "44313054596", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "Nome não pode ser nulo ou vazio.")]
    [InlineData(1, "", "44313054596", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "Nome não pode ser nulo ou vazio.")]
    [InlineData(1, "Teste", null, "email@teste.com", "11965481203", "Masculino", "1990-11-04", "CPF não pode ser nulo ou vazio.")]
    [InlineData(1, "Teste", "", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "CPF não pode ser nulo ou vazio.")]
    [InlineData(1, "Teste", "4431305", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "CPF deve conter exatamente 11 digitos.")]
    [InlineData(1, "Teste", "4431305ac4", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "CPF deve conter exatamente 11 digitos.")]
    [InlineData(1, "Teste", "44313050124", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "CPF inválido.")]
    [InlineData(1, "Teste", "11111111111", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "CPF inválido.")]
    [InlineData(1, "Teste", "44313054596", null, "11965481203", "Masculino", "1990-11-04", "Email não pode ser nulo ou vazio.")]
    [InlineData(1, "Teste", "44313054596", "", "11965481203", "Masculino", "1990-11-04", "Email não pode ser nulo ou vazio.")]
    [InlineData(1, "Teste", "44313054596", "teste@", "11965481203", "Masculino", "1990-11-04", "Email com valor inválido.")]
    [InlineData(1, "Teste", "44313054596", "email@teste.com", null, "Masculino", "1990-11-04", "Telefone não pode ser nulo ou vazio.")]
    [InlineData(1, "Teste", "44313054596", "email@teste.com", "", "Masculino", "1990-11-04", "Telefone não pode ser nulo ou vazio.")]
    [InlineData(1, "Teste", "44313054596", "email@teste.com", "invalid-phone", "Masculino", "1990-11-04", "Telefone não pode ser nulo ou vazio.")]
    [InlineData(1, "Teste", "44313054596", "email@teste.com", "12345", "Masculino", "1990-11-04", "Telefone deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    [InlineData(1, "Teste", "44313054596", "email@teste.com", "123456789012", "Masculino", "1990-11-04", "Telefone deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    [InlineData(1, "Teste", "44313054596", "email@teste.com", "11965481203", null, "1990-11-04", "Genero não pode ser nulo ou vazio.")]
    [InlineData(1, "Teste", "44313054596", "email@teste.com", "11965481203", "", "1990-11-04", "Genero não pode ser nulo ou vazio.")]
    [InlineData(1, "Teste", "44313054596", "email@teste.com", "11965481203", "genero", "1990-11-04", "Genero inválido. Valores válidos: 'Masculino, Feminino, Não informado'")]
    [InlineData(1, "Teste", "44313054596", "email@teste.com", "11965481203", "Masculino", null, "DataNascimento não pode ser nulo ou vazio.")]
    [InlineData(1, "Teste", "44313054596", "email@teste.com", "11965481203", "Masculino", "", "DataNascimento não pode ser nulo ou vazio.")]
    [InlineData(1, "Teste", "44313054596", "email@teste.com", "11965481203", "Masculino", "invalid-date", "DataNascimento está em um formato inválido. O formato correto é yyyy-MM-dd.")]
    [InlineData(1, "Teste", "44313054596", "email@teste.com", "11965481203", "Masculino", "2150-12-31", "DataNascimento deve ser uma data passada e menor que hoje.")]
    public void PacienteDependente_ShouldThrowException_WhenAddNew_FieldsAreInvalids(int pacientePrincipalId, string nome, string cpf, string email, string telefone, string genero, string dataNascimento, string expectedMessage)
    {
        // Arrange & Act
        var ex = Assert.Throws<EntityValidationException>(() =>
            new PacienteDependente(
                pacientePrincipalId: pacientePrincipalId,
                nome: nome,
                cpf: cpf,
                email: email,
                telefone: telefone,
                genero: genero,
                dataNascimento: dataNascimento,
                nomeSocial: _faker.Person.FirstName,
                peso: _faker.CustomWeight(),
                altura: _faker.CustomHeight()
        ));

        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }


    [Theory]
    [InlineData(null, "44313054596", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "Nome não pode ser nulo ou vazio.")]
    [InlineData("", "44313054596", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "Nome não pode ser nulo ou vazio.")]
    [InlineData("Teste", null, "email@teste.com", "11965481203", "Masculino", "1990-11-04", "CPF não pode ser nulo ou vazio.")]
    [InlineData("Teste", "", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "CPF não pode ser nulo ou vazio.")]
    [InlineData("Teste", "4431305", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "CPF deve conter exatamente 11 digitos.")]
    [InlineData("Teste", "4431305ac4", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "CPF deve conter exatamente 11 digitos.")]
    [InlineData("Teste", "44313050124", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "CPF inválido.")]
    [InlineData("Teste", "11111111111", "email@teste.com", "11965481203", "Masculino", "1990-11-04", "CPF inválido.")]
    [InlineData("Teste", "44313054596", null, "11965481203", "Masculino", "1990-11-04", "Email não pode ser nulo ou vazio.")]
    [InlineData("Teste", "44313054596", "", "11965481203", "Masculino", "1990-11-04", "Email não pode ser nulo ou vazio.")]
    [InlineData("Teste", "44313054596", "teste@", "11965481203", "Masculino", "1990-11-04", "Email com valor inválido.")]
    [InlineData("Teste", "44313054596", "email@teste.com", null, "Masculino", "1990-11-04", "Telefone não pode ser nulo ou vazio.")]
    [InlineData("Teste", "44313054596", "email@teste.com", "", "Masculino", "1990-11-04", "Telefone não pode ser nulo ou vazio.")]
    [InlineData("Teste", "44313054596", "email@teste.com", "invalid-phone", "Masculino", "1990-11-04", "Telefone não pode ser nulo ou vazio.")]
    [InlineData("Teste", "44313054596", "email@teste.com", "12345", "Masculino", "1990-11-04", "Telefone deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    [InlineData("Teste", "44313054596", "email@teste.com", "123456789012", "Masculino", "1990-11-04", "Telefone deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    [InlineData("Teste", "44313054596", "email@teste.com", "11965481203", null, "1990-11-04", "Genero não pode ser nulo ou vazio.")]
    [InlineData("Teste", "44313054596", "email@teste.com", "11965481203", "", "1990-11-04", "Genero não pode ser nulo ou vazio.")]
    [InlineData("Teste", "44313054596", "email@teste.com", "11965481203", "genero", "1990-11-04", "Genero inválido. Valores válidos: 'Masculino, Feminino, Não informado'")]
    [InlineData("Teste", "44313054596", "email@teste.com", "11965481203", "Masculino", null, "DataNascimento não pode ser nulo ou vazio.")]
    [InlineData("Teste", "44313054596", "email@teste.com", "11965481203", "Masculino", "", "DataNascimento não pode ser nulo ou vazio.")]
    [InlineData("Teste", "44313054596", "email@teste.com", "11965481203", "Masculino", "invalid-date", "DataNascimento está em um formato inválido. O formato correto é yyyy-MM-dd.")]
    [InlineData("Teste", "44313054596", "email@teste.com", "11965481203", "Masculino", "2150-12-31", "DataNascimento deve ser uma data passada e menor que hoje.")]
    public void PacienteDependente_ShouldThrowException_WhenUpdate_FieldsAreInvalids(string nome, string cpf, string email, string telefone, string genero, string dataNascimento, string expectedMessage)
    {
        // Arrange
        var obj = new PacienteDependente(
            pacientePrincipalId: 1,
            nome: _faker.Person.FullName,
            cpf: _faker.Person.Cpf(false),
            email: _faker.Internet.Email(),
            telefone: _faker.Person.CustomCellPhoneBR(),
            genero: _faker.CustomGender(),
            dataNascimento: _faker.CustomDateOfBirth(20),
            nomeSocial: _faker.Person.FirstName,
            peso: _faker.CustomWeight(),
            altura: _faker.CustomHeight()
        );

        // Act
        var ex = Assert.Throws<EntityValidationException>(() =>
            obj.Update(
                nome: nome,
                cpf: cpf,
                email: email,
                telefone: telefone,
                genero: genero,
                dataNascimento: dataNascimento,
                nomeSocial: _faker.Person.FirstName,
                peso: _faker.CustomWeight(),
                altura: _faker.CustomHeight())
        );

        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }


}



