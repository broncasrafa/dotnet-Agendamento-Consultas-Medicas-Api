using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using RSF.AgendamentoConsultas.Core.Domain.Tests.Base.Extensions;
using FluentAssertions;
using Bogus;
using Bogus.Extensions.Brazil;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.Entities;

public class ApplicationUserTest
{
    private readonly Faker _faker;

    public ApplicationUserTest() => _faker = new Faker("pt_BR");


    [Fact]
    public void ApplicationUser_ShouldBeValid_WhenAddNew_AllFieldsIsValid()
    {
        // Arrange
        var obj = new ApplicationUser
        (
            nomeCompleto: _faker.Person.FullName,
            username:  _faker.Person.UserName,
            documento:  _faker.Person.Cpf(false),
            email: _faker.Internet.Email(),
            genero: _faker.CustomGender(),
            telefone: _faker.Person.CustomCellPhoneBR()
        );

        // Act & Assert
        FluentActions.Invoking(() => obj).Should().NotThrow();

        obj.Should().NotBeNull();
    }

    [Fact]
    public void ApplicationUser_ShouldBeValid_WhenUpdate_AllFieldsIsValid()
    {
        // Arrange
        var obj = new ApplicationUser
        (
            nomeCompleto: _faker.Person.FullName,
            username: _faker.Person.UserName,
            documento: _faker.Person.Cpf(false),
            email: _faker.Internet.Email(),
            genero: _faker.CustomGender(),
            telefone: _faker.Person.CustomCellPhoneBR()
        );

        // Act & Assert
        FluentActions.Invoking(() => 
            obj.Update(
                nomeCompleto: _faker.Person.FullName,
                telefone: _faker.Person.CustomCellPhoneBR(), 
                email: _faker.Internet.Email(), 
                username: _faker.Person.UserName, 
                isEmailChanged: true)
        ).Should().NotThrow();

        obj.Should().NotBeNull();
        obj.UpdatedAt.Should().NotBeNull();
        obj.EmailConfirmed.Should().BeFalse();
    }


    [Theory]
    [InlineData(null, "username", "27568773280", "teste@teste.com", "Masculino", "11987456320", "NomeCompleto não pode ser nulo ou vazio.")]
    [InlineData("", "username", "27568773280", "teste@teste.com", "Masculino", "11987456320", "NomeCompleto não pode ser nulo ou vazio.")]
    [InlineData("Teste", "username", "27568773280", "teste@teste.com", "Masculino", "11987456320", "NomeCompleto inválido.")]
    [InlineData("Teste Cristino", "username", null, "teste@teste.com", "Masculino", "11987456320", "Documento não pode ser nulo ou vazio.")]
    [InlineData("Teste Cristino", "username", "", "teste@teste.com", "Masculino", "11987456320", "Documento não pode ser nulo ou vazio.")]
    [InlineData("Teste Cristino", "username", "27568773280", null, "Masculino", "11987456320", "Email não pode ser nulo ou vazio.")]
    [InlineData("Teste Cristino", "username", "27568773280", "", "Masculino", "11987456320", "Email não pode ser nulo ou vazio.")]
    [InlineData("Teste Cristino", "username", "27568773280", "teste@", "Masculino", "11987456320", "Email com valor inválido.")]
    [InlineData("Teste Cristino", null, "27568773280", "teste@teste.com", "Masculino", "11987456320", "UserName não pode ser nulo ou vazio.")]
    [InlineData("Teste Cristino", "", "27568773280", "teste@teste.com", "Masculino", "11987456320", "UserName não pode ser nulo ou vazio.")]
    [InlineData("Teste Cristino", "username", "27568773280", "teste@teste.com", "Masculino", null, "PhoneNumber não pode ser nulo ou vazio.")]
    [InlineData("Teste Cristino", "username", "27568773280", "teste@teste.com", "Masculino", "", "PhoneNumber não pode ser nulo ou vazio.")]
    [InlineData("Teste Cristino", "username", "27568773280", "teste@teste.com", "Masculino", "invalid-phone", "PhoneNumber não pode ser nulo ou vazio.")]
    [InlineData("Teste Cristino", "username", "27568773280", "teste@teste.com", "Masculino", "12345", "PhoneNumber deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    [InlineData("Teste Cristino", "username", "27568773280", "teste@teste.com", "Masculino", "123456789012", "PhoneNumber deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    [InlineData("Teste Cristino", "username", "27568773280", "teste@teste.com", null, "11987456320", "Genero não pode ser nulo ou vazio.")]
    [InlineData("Teste Cristino", "username", "27568773280", "teste@teste.com", "", "11987456320", "Genero não pode ser nulo ou vazio.")]
    [InlineData("Teste Cristino", "username", "27568773280", "teste@teste.com", "Viado", "11987456320", "Genero inválido. Valores válidos: 'Masculino, Feminino, Não informado'")]

    public void ApplicationUser_ShouldThrowException_WhenAddNew_FieldsAreInvalid(string nomeCompleto, string username, string documento, string email, string genero, string telefone, string expectedMessage)
    {
        // Arrange & Act
        var ex = Assert.Throws<EntityValidationException>(() =>
            new ApplicationUser(nomeCompleto, username, documento, email, genero, telefone));

        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }



    [Theory]
    [InlineData(null, "username", "teste@teste.com", "11987456320", "NomeCompleto não pode ser nulo ou vazio.")]
    [InlineData("", "username", "teste@teste.com", "11987456320", "NomeCompleto não pode ser nulo ou vazio.")]
    [InlineData("Teste", "username", "teste@teste.com", "11987456320", "NomeCompleto inválido.")]
    [InlineData("Teste Cristino", null, "teste@teste.com", "11987456320", "UserName não pode ser nulo ou vazio.")]
    [InlineData("Teste Cristino", "", "teste@teste.com", "11987456320", "UserName não pode ser nulo ou vazio.")]
    [InlineData("Teste Cristino", "username", null, "11987456320", "Email não pode ser nulo ou vazio.")]
    [InlineData("Teste Cristino", "username", "", "11987456320", "Email não pode ser nulo ou vazio.")]
    [InlineData("Teste Cristino", "username", "teste@", "11987456320", "Email com valor inválido.")]
    [InlineData("Teste Cristino", "username", "teste@teste.com", null, "PhoneNumber não pode ser nulo ou vazio.")]
    [InlineData("Teste Cristino", "username", "teste@teste.com", "", "PhoneNumber não pode ser nulo ou vazio.")]
    [InlineData("Teste Cristino", "username", "teste@teste.com", "invalid-phone", "PhoneNumber não pode ser nulo ou vazio.")]
    [InlineData("Teste Cristino", "username", "teste@teste.com", "12345", "PhoneNumber deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    [InlineData("Teste Cristino", "username", "teste@teste.com", "123456789012", "PhoneNumber deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    public void ApplicationUser_ShouldThrowException_WhenUpdate_FieldsAreInvalid(string nomeCompleto, string username, string email, string telefone, string expectedMessage)
    {
        // Arrange
        var obj = new ApplicationUser
        (
            nomeCompleto: _faker.Person.FullName,
            username: _faker.Person.UserName,
            documento: _faker.Person.Cpf(false),
            email: _faker.Internet.Email(),
            genero: _faker.CustomGender(),
            telefone: _faker.Person.CustomCellPhoneBR()
        );

        // Arrange & Act
        var ex = Assert.Throws<EntityValidationException>(() =>
            obj.Update(
                nomeCompleto: nomeCompleto,
                telefone: telefone,
                email: email,
                username: username,
                isEmailChanged: true));

        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }
}
