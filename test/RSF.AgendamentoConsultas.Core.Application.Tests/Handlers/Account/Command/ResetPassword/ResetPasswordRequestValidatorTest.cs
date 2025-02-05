using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ResetPassword;
using FluentAssertions;
using Bogus;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Account.Command.ResetPassword;

public class ResetPasswordRequestValidatorTest
{
    private readonly Faker _faker;
    private readonly ResetPasswordRequestValidator _validator;

    public ResetPasswordRequestValidatorTest()
    {
        _faker = new Faker(locale: "pt_BR");
        _validator = new ResetPasswordRequestValidator();
    }

    [Fact(DisplayName = "Deve validar os dados de entrada com resultado de sucesso")]
    public void Validate_ShouldBeValid_When_AllInputs_IsValid()
    {
        // Arrange
        var request = new ResetPasswordRequest(
                Email: _faker.Person.Email,
                ResetCode: "EyCqDm97SgPzGH9tmrNCPEQe",
                NewPassword: "Usuario@123");
        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
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
        var request = new ResetPasswordRequest(
                Email: email,
                ResetCode: "EyCqDm97SgPzGH9tmrNCPEQe",
                NewPassword: "Usuario@123");

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }


    [Theory(DisplayName = "Deve retornar erro ao validar o ResetCode")]
    [InlineData(null, "O Código de Reset de senha é obrigatório, não deve ser nulo ou vazio")]
    [InlineData("", "O Código de Reset de senha é obrigatório, não deve ser nulo ou vazio")]
    public void Validate_ShouldHaveError_When_ResetCode_IsInvalid(string resetCode, string expectedMessage)
    {
        //Arrange
        var request = new ResetPasswordRequest(
                Email: _faker.Person.Email,
                ResetCode: resetCode,
                NewPassword: "Usuario@123");

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }


    [Theory(DisplayName = "Deve retornar erro ao validar o NewPassword")]
    [InlineData(null, "Senha é obrigatório, não deve ser nulo ou vazia")]
    [InlineData("", "Senha é obrigatório, não deve ser nulo ou vazia")]
    [InlineData("Se@1", "Senha deve ter pelo menos 5 caracteres")]
    [InlineData("senha@123", "Senha deve ter pelo menos 1 letra maiuscula")]
    [InlineData("SENHA@123", "Senha deve ter pelo menos 1 letra minuscula")]
    [InlineData("Senha@@teste", "Senha deve ter pelo menos 1 número")]
    [InlineData("Senha123", "Senha deve ter pelo menos 1 caracter especial")]
    public void Validate_ShouldHaveError_When_NewPassword_IsInvalid(string newPassword, string expectedMessage)
    {
        //Arrange
        var request = new ResetPasswordRequest(
                Email: _faker.Person.Email,
                ResetCode: "EyCqDm97SgPzGH9tmrNCPEQe",
                NewPassword: newPassword);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }
}