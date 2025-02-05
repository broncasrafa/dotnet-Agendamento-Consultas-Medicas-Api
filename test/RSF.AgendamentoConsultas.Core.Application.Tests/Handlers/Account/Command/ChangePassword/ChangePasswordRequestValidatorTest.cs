using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ChangePassword;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Account.Command.ChangePassword;

public class ChangePasswordRequestValidatorTest
{
    private readonly ChangePasswordRequestValidator _validator;

    public ChangePasswordRequestValidatorTest() => _validator = new ChangePasswordRequestValidator();

    [Fact]
    public void Validate_ShouldBeValid_WhenAllFieldsAreValid()
    {
        var result = _validator.Validate(new ChangePasswordRequest(NewPassword: "NovaSenha@123", OldPassword: "SenhaAntiga@123"));
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("NovaSenha@123", null, "A senha antiga é obrigatória, não deve ser nulo ou vazia")]
    [InlineData("NovaSenha@123", "", "A senha antiga é obrigatória, não deve ser nulo ou vazia")]
    [InlineData(null, "SenhaAntiga@123", "Senha é obrigatório, não deve ser nulo ou vazia")]
    [InlineData("", "SenhaAntiga@123", "Senha é obrigatório, não deve ser nulo ou vazia")]
    [InlineData("No@1", "SenhaAntiga@123", "Senha deve ter pelo menos 5 caracteres")]
    [InlineData("novasenha@123", "SenhaAntiga@123", "Senha deve ter pelo menos 1 letra maiuscula")]
    [InlineData("NOVASENHA@123", "SenhaAntiga@123", "Senha deve ter pelo menos 1 letra minuscula")]
    [InlineData("NovaSenha@@teste", "SenhaAntiga@123", "Senha deve ter pelo menos 1 número")]
    [InlineData("NovaSenha123", "SenhaAntiga@123", "Senha deve ter pelo menos 1 caracter especial")]
    public void Validate_ShouldHaveErrors_When_FieldsAreInvalids(string newPassword, string oldPassword, string expectedMessage)
    {
        // Arrange
        var request = new ChangePasswordRequest(newPassword, oldPassword);
        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }
}