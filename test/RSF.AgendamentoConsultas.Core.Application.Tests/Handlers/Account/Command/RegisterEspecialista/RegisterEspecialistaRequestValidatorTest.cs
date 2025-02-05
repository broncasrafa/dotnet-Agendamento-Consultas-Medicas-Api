using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.RegisterEspecialista;
using RSF.AgendamentoConsultas.Core.Application.Tests.Base.Extensions;
using FluentAssertions;
using Bogus;
using Bogus.Extensions.Belgium;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Account.Command.RegisterEspecialista;

public class RegisterEspecialistaRequestValidatorTest
{
    private readonly Faker _faker;
    private readonly RegisterEspecialistaRequestValidator _validator;

    public RegisterEspecialistaRequestValidatorTest()
    {
        _faker = new Faker(locale: "pt_BR");
        _validator = new RegisterEspecialistaRequestValidator();
    }


    [Fact(DisplayName = "Deve validar os dados de entrada com resultado de sucesso")]
    public void Validate_ShouldBeValid_When_AllInputs_IsValid()
    {
        // Arrange
        var request = new RegisterEspecialistaRequest(
                NomeCompleto: _faker.Person.FullName,
                Username: _faker.Person.UserName,
                Email: _faker.Person.Email,
                TipoCategoria: _faker.PickRandom(new string[] { "BASIC", "PREMIUM" }),
                EspecialidadeId: 1,
                Genero: _faker.CustomGender(),
                Licenca: _faker.Person.NationalNumber(),
                Telefone: _faker.Person.CustomCellPhoneBR(),
                Password: "Usuario@123",
                ConfirmPassword: "Usuario@123");

        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }


    [Theory(DisplayName = "Deve retornar erro ao validar o EspecialidadeId")]
    [InlineData(0, "O ID do Especialidade deve ser maior que 0")]
    [InlineData(-1, "O ID do Especialidade deve ser maior que 0")]
    public void Validate_ShouldHaveError_When_EspecialidadeId_IsInvalid(int EspecialidadeId, string expectedMessage)
    {
        //Arrange
        var request = new RegisterEspecialistaRequest(
                NomeCompleto: _faker.Person.FullName,
                Username: _faker.Person.UserName,
                Email: _faker.Person.Email,
                TipoCategoria: _faker.PickRandom(new string[] { "BASIC", "PREMIUM" }),
                EspecialidadeId: EspecialidadeId,
                Genero: _faker.CustomGender(),
                Licenca: _faker.Person.NationalNumber(),
                Telefone: _faker.Person.CustomCellPhoneBR(),
                Password: "Usuario@123",
                ConfirmPassword: "Usuario@123");

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }


    [Theory(DisplayName = "Deve retornar erro ao validar o NomeCompleto")]
    [InlineData(null, "O nome completo do usuário é obrigatório")]
    [InlineData("", "O nome completo do usuário é obrigatório")]
    [InlineData("Teste", "O nome completo do usuário deve conter pelo menos um sobrenome.")]
    public void Validate_ShouldHaveError_When_NomeCompleto_IsInvalid(string nomeCompleto, string expectedMessage)
    {
        //Arrange
        var request = new RegisterEspecialistaRequest(
                NomeCompleto: nomeCompleto,
                Username: _faker.Person.UserName,
                Email: _faker.Person.Email,
                TipoCategoria: _faker.PickRandom(new string[] { "BASIC", "PREMIUM" }),
                EspecialidadeId: 1,
                Genero: _faker.CustomGender(),
                Licenca: _faker.Person.NationalNumber(),
                Telefone: _faker.Person.CustomCellPhoneBR(),
                Password: "Usuario@123",
                ConfirmPassword: "Usuario@123");

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
        var request = new RegisterEspecialistaRequest(
                NomeCompleto: _faker.Person.FullName,
                Username: Username,
                Email: _faker.Person.Email,
                TipoCategoria: _faker.PickRandom(new string[] { "BASIC", "PREMIUM" }),
                EspecialidadeId: 1,
                Genero: _faker.CustomGender(),
                Licenca: _faker.Person.NationalNumber(),
                Telefone: _faker.Person.CustomCellPhoneBR(),
                Password: "Usuario@123",
                ConfirmPassword: "Usuario@123");

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
        var request = new RegisterEspecialistaRequest(
                NomeCompleto: _faker.Person.FullName,
                Username: _faker.Person.UserName,
                Email: email,
                TipoCategoria: _faker.PickRandom(new string[] { "BASIC", "PREMIUM" }),
                EspecialidadeId: 1,
                Genero: _faker.CustomGender(),
                Licenca: _faker.Person.NationalNumber(),
                Telefone: _faker.Person.CustomCellPhoneBR(),
                Password: "Usuario@123",
                ConfirmPassword: "Usuario@123");

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }


    [Theory(DisplayName = "Deve retornar erro ao validar o Genero")]
    [InlineData(null, "O gênero não pode ser nulo ou vazio.")]
    [InlineData("", "O gênero não pode ser nulo ou vazio.")]
    [InlineData("Todes", "O gênero deve ser 'Masculino' ou 'Feminino'.")]
    public void Validate_ShouldHaveError_When_Genero_IsInvalid(string genero, string expectedMessage)
    {
        //Arrange
        var request = new RegisterEspecialistaRequest(
                NomeCompleto: _faker.Person.FullName,
                Username: _faker.Person.UserName,
                Email: _faker.Person.Email,
                TipoCategoria: _faker.PickRandom(new string[] { "BASIC", "PREMIUM" }),
                EspecialidadeId: 1,
                Genero: genero,
                Licenca: _faker.Person.NationalNumber(),
                Telefone: _faker.Person.CustomCellPhoneBR(),
                Password: "Usuario@123",
                ConfirmPassword: "Usuario@123");

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }


    [Theory(DisplayName = "Deve retornar erro ao validar o TipoCategoria")]
    [InlineData(null, "O Tipo de categoria é obrigatório, não deve ser nulo ou vazio")]
    [InlineData("", "O Tipo de categoria é obrigatório, não deve ser nulo ou vazio")]
    [InlineData("Teste", "O Tipo de categoria deve ser 'Basic' ou 'Premium'.")]
    [InlineData("Paciente", "O Tipo de categoria deve ser 'Basic' ou 'Premium'.")]
    public void Validate_ShouldHaveError_When_TipoCategoria_IsInvalid(string TipoCategoria, string expectedMessage)
    {
        //Arrange
        var request = new RegisterEspecialistaRequest(
                NomeCompleto: _faker.Person.FullName,
                Username: _faker.Person.UserName,
                Email: _faker.Person.Email,
                TipoCategoria: TipoCategoria,
                EspecialidadeId: 1,
                Genero: _faker.CustomGender(),
                Licenca: _faker.Person.NationalNumber(),
                Telefone: _faker.Person.CustomCellPhoneBR(),
                Password: "Usuario@123",
                ConfirmPassword: "Usuario@123");

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }


    [Theory(DisplayName = "Deve retornar erro ao validar o Licenca")]
    [InlineData(null, "A Licença é obrigatória, não deve ser nulo ou vazia")]
    [InlineData("", "A Licença é obrigatória, não deve ser nulo ou vazia")]
    [InlineData("test", "A Licença deve ter no mínimo 5 caracteres")]
    public void Validate_ShouldHaveError_When_Licenca_IsInvalid(string Licenca, string expectedMessage)
    {
        //Arrange
        var request = new RegisterEspecialistaRequest(
                NomeCompleto: _faker.Person.FullName,
                Username: _faker.Person.UserName,
                Email: _faker.Person.Email,
                TipoCategoria: _faker.PickRandom(new string[] { "BASIC", "PREMIUM" }),
                EspecialidadeId: 1,
                Genero: _faker.CustomGender(),
                Licenca: Licenca,
                Telefone: _faker.Person.CustomCellPhoneBR(),
                Password: "Usuario@123",
                ConfirmPassword: "Usuario@123");

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
        var request = new RegisterEspecialistaRequest(
                NomeCompleto: _faker.Person.FullName,
                Username: _faker.Person.UserName,
                Email: _faker.Person.Email,
                TipoCategoria: _faker.PickRandom(new string[] { "BASIC", "PREMIUM" }),
                EspecialidadeId: 1,
                Genero: _faker.CustomGender(),
                Licenca: _faker.Person.NationalNumber(),
                Telefone: telefone,
                Password: "Usuario@123",
                ConfirmPassword: "Usuario@123");

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }


    [Theory(DisplayName = "Deve retornar erro ao validar o Password")]
    [InlineData(null, "Senha é obrigatório, não deve ser nulo ou vazia")]
    [InlineData("", "Senha é obrigatório, não deve ser nulo ou vazia")]
    [InlineData("Se@1", "Senha deve ter pelo menos 5 caracteres")]
    [InlineData("senha@123", "Senha deve ter pelo menos 1 letra maiuscula")]
    [InlineData("SENHA@123", "Senha deve ter pelo menos 1 letra minuscula")]
    [InlineData("Senha@@teste", "Senha deve ter pelo menos 1 número")]
    [InlineData("Senha123", "Senha deve ter pelo menos 1 caracter especial")]
    public void Validate_ShouldHaveError_When_Password_IsInvalid(string password, string expectedMessage)
    {
        //Arrange
        var request = new RegisterEspecialistaRequest(
                NomeCompleto: _faker.Person.FullName,
                Username: _faker.Person.UserName,
                Email: _faker.Person.Email,
                TipoCategoria: _faker.PickRandom(new string[] { "BASIC", "PREMIUM" }),
                EspecialidadeId: 1,
                Genero: _faker.CustomGender(),
                Licenca: _faker.Person.NationalNumber(),
                Telefone: _faker.Person.CustomCellPhoneBR(),
                Password: password,
                ConfirmPassword: password);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }


    [Theory(DisplayName = "Deve retornar erro ao validar o ConfirmPassword")]
    [InlineData("Senha@123", null, "Senha de confirmação é obrigatório, não deve ser nulo ou vazia")]
    [InlineData("Senha@123", "", "Senha de confirmação é obrigatório, não deve ser nulo ou vazia")]
    [InlineData("Senha@123", "Se@1", "Senha de confirmação deve ter pelo menos 5 caracteres")]
    [InlineData("Senha@123", "senha@123", "Senha de confirmação deve ter pelo menos 1 letra maiuscula")]
    [InlineData("Senha@123", "SENHA@123", "Senha de confirmação deve ter pelo menos 1 letra minuscula")]
    [InlineData("Senha@123", "Senha@@teste", "Senha de confirmação deve ter pelo menos 1 número")]
    [InlineData("Senha@123", "Senha123", "Senha de confirmação deve ter pelo menos 1 caracter especial")]
    [InlineData("Senha@123", "Senha@12345", "Senha de confirmação não confere com a senha escolhida")]
    public void Validate_ShouldHaveError_When_ConfirmPassword_IsInvalid(string password, string confirmPassword, string expectedMessage)
    {
        //Arrange
        var request = new RegisterEspecialistaRequest(
                NomeCompleto: _faker.Person.FullName,
                Username: _faker.Person.UserName,
                Email: _faker.Person.Email,
                TipoCategoria: _faker.PickRandom(new string[] { "BASIC", "PREMIUM" }),
                EspecialidadeId: 1,
                Genero: _faker.CustomGender(),
                Licenca: _faker.Person.NationalNumber(),
                Telefone: _faker.Person.CustomCellPhoneBR(),
                Password: password,
                ConfirmPassword: confirmPassword);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }
}