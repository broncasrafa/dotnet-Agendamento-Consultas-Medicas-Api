using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.Login;
using Bogus;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Account.Command.Login;

public class LoginRequestValidatorTest
{
    private readonly Faker _faker;
    private readonly LoginRequestValidator _validator;

    public LoginRequestValidatorTest()
    {
        _faker = new Faker(locale: "pt_BR");
        _validator = new LoginRequestValidator();
    }

    [Fact]
    public void Validate_ShouldBeValid_When_InputIsValid()
    {
        var request = new LoginRequest(Email: _faker.Person.Email, Password: "Usuario@123");
        var result = _validator.Validate(request);
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData(null, "Usuario@123", "E-mail é obrigatório, não deve ser nulo ou vazio")]
    [InlineData("", "Usuario@123", "E-mail é obrigatório, não deve ser nulo ou vazio")]
    [InlineData("ajh36cmebc0wwwtm5ib9f0ai4e3wac4x7tbkdv8r7mr4da0t43ke2fefe5pj8j4mfuxm0inn79a2ivw2buq1upc0rxvhyvcqpw1nr8puu9uu6n1zxvw8e5g5qf6xpe9e1z9cie6j1vzr4bxqj6yq6tqme55825iyttvkckw0wufm6n0qg973bgac1ct960@teste.com.br", "Usuario@123", "E-mail não deve exceder 200 caracteres")]
    [InlineData("teste", "Usuario@123", "E-mail inválido")]
    [InlineData("teste@", "Usuario@123", "E-mail inválido")]
    [InlineData("usuario.com.br", "Usuario@123", "E-mail inválido")]
    [InlineData("@teste", "Usuario@123", "E-mail inválido")]
    [InlineData("teste@teste.com.br", null, "Senha é obrigatório, não deve ser nulo ou vazia")]
    [InlineData("teste@teste.com.br", "", "Senha é obrigatório, não deve ser nulo ou vazia")]
    public void Validate_ShouldHaveErrors_When_InputIsInvalid(string email, string password, string expectedMessage)
    {
        // Arrange
        var request = new LoginRequest(Email: email, Password: password);
        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }
}