using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.UpdateAuthenticatedUserInfo;
using RSF.AgendamentoConsultas.Core.Application.Tests.Base.Extensions;
using FluentAssertions;
using Bogus;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Account.Command.UpdateAuthenticatedUserInfo;

public class UpdateAuthenticatedUserInfoRequestValidatorTest
{
    private readonly Faker _faker;
    private readonly UpdateAuthenticatedUserInfoRequestValidator _validator;

    public UpdateAuthenticatedUserInfoRequestValidatorTest()
    {
        _faker = new Faker(locale: "pt_BR");
        _validator = new UpdateAuthenticatedUserInfoRequestValidator();
    }



    [Fact(DisplayName = "Deve validar os dados de entrada com resultado de sucesso")]
    public void Validate_ShouldBeValid_When_AllInputs_IsValid()
    {
        // Arrange
        var request = new UpdateAuthenticatedUserInfoRequest(
                NomeCompleto: _faker.Person.FullName,
                Telefone: _faker.Person.CustomCellPhoneBR(),
                Email: _faker.Person.Email,
                Username: _faker.Person.UserName);

        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }


    [Theory(DisplayName = "Deve retornar erro ao validar o NomeCompleto")]
    [InlineData(null, "O nome completo do usuário é obrigatório")]
    [InlineData("", "O nome completo do usuário é obrigatório")]
    [InlineData("Teste", "O nome completo do usuário deve conter pelo menos um sobrenome.")]
    public void Validate_ShouldHaveError_When_NomeCompleto_IsInvalid(string nomeCompleto, string expectedMessage)
    {
        //Arrange
        var request = new UpdateAuthenticatedUserInfoRequest(
                NomeCompleto: nomeCompleto,
                Telefone: _faker.Person.CustomCellPhoneBR(),
                Email: _faker.Person.Email,
                Username: _faker.Person.UserName);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }


    [Theory(DisplayName = "Deve retornar erro ao validar o Telefone")]
    [InlineData(null, "O telefone não pode ser nulo ou vazio.")]
    [InlineData("", "O telefone não pode ser nulo ou vazio.")]
    [InlineData("invalid-phone", "O telefone deve conter apenas números.")]
    [InlineData("12345", "O telefone deve ter 10 dígitos para fixo ou 11 dígitos para celular.")]
    [InlineData("123456789012", "O telefone deve ter 10 dígitos para fixo ou 11 dígitos para celular.")]
    [InlineData("11111111111", "O telefone não deve ter somente números consecutivos iguais")]
    public void Validate_ShouldHaveError_When_Telefone_IsInvalid(string telefone, string expectedMessage)
    {
        //Arrange
        var request = new UpdateAuthenticatedUserInfoRequest(
                NomeCompleto: _faker.Person.FullName,
                Telefone: telefone,
                Email: _faker.Person.Email,
                Username: _faker.Person.UserName);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }


    [Theory(DisplayName = "Deve retornar erro ao validar o E-mail")]
    [InlineData(null, "E-mail é obrigatório, não deve ser nulo ou vazio")]
    [InlineData("", "E-mail é obrigatório, não deve ser nulo ou vazio")]
    [InlineData("ajh36cmebc0wwwtm5ib9f0ai4e3wac4x7tbkdv8r7mr4da0t43ke2fefe5pj8j4mfuxm0inn79a2ivw2buq1upc0rxvhyvcqpw1nr8puu9uu6n1zxvw8e5g5qf6xpe9e1z9cie6j1vzr4bxqj6yq6tqme55825iyttvkckw0wufm6n0qg973bgac1ct960@teste.com.br", "E-mail não deve exceder 200 caracteres")]
    [InlineData("teste", "E-mail inválido")]
    [InlineData("teste@", "E-mail inválido")]
    [InlineData("usuario.com.br", "E-mail inválido")]
    [InlineData("@teste", "E-mail inválido")]
    public void Validate_ShouldHaveError_When_Email_IsInvalid(string email, string expectedMessage)
    {
        //Arrange
        var request = new UpdateAuthenticatedUserInfoRequest(
                NomeCompleto: _faker.Person.FullName,
                Telefone: _faker.Person.CustomCellPhoneBR(),
                Email: email,
                Username: _faker.Person.UserName);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }


    [Theory(DisplayName = "Deve retornar erro ao validar o Username")]
    [InlineData(null, "O Username é obrigatório, não deve ser nulo ou vazio")]
    [InlineData("", "O Username é obrigatório, não deve ser nulo ou vazio")]
    [InlineData("teste", "O Username deve ter no mínimo 6 caracteres")]
    public void Validate_ShouldHaveError_When_Username_IsInvalid(string Username, string expectedMessage)
    {
        //Arrange
        var request = new UpdateAuthenticatedUserInfoRequest(
                NomeCompleto: _faker.Person.FullName,
                Telefone: _faker.Person.CustomCellPhoneBR(),
                Email: _faker.Person.Email,
                Username: Username);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }
}