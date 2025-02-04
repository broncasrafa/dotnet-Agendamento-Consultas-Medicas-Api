using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ConfirmEmailResend;
using FluentAssertions;
using Bogus;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Account.Command.ConfirmEmailResend;

public class ConfirmEmailResendRequestValidatorTest
{
    private readonly Faker _faker;
    private readonly ConfirmEmailResendRequestValidator _validator;

    public ConfirmEmailResendRequestValidatorTest()
    {
        _faker = new Faker(locale: "pt_BR");
        _validator = new ConfirmEmailResendRequestValidator();
    }

    [Fact]
    public void Validate_ShouldBeValid_When_InputIsValid()
    {
        var request = new ConfirmEmailResendRequest(Email: _faker.Person.Email);
        var result = _validator.Validate(request);
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData(null, "E-mail é obrigatório, não deve ser nulo ou vazio")]
    [InlineData("", "E-mail é obrigatório, não deve ser nulo ou vazio")]
    [InlineData("riley_reid.gabily_castro_gemeas_lara_e_larissa_joy_ski_agatha_mama_edna_samara_vivy_marques_pamela_pantera_aylla_mel_luara_sonza_bea_cherry_vivian_lolita_isa_fox_bruna_santos_patricia_ferraz_renatinha_pantera_rita_almeida_charmille.mia_khalifa_lana_rhodes_violet_myers.asa_akira.madison_ivy.august_ames.alexis_texas.abella_danger_.nicole_aniston.lena_paul.mia_malkova@br.gestant.com", "E-mail não deve exceder 200 caracteres")]
    [InlineData("teste", "E-mail inválido")]
    [InlineData("teste@", "E-mail inválido")]
    [InlineData("usuario.com.br", "E-mail inválido")]
    [InlineData("@teste", "E-mail inválido")]
    public void Validate_ShouldHaveErrors_When_InputIsInvalid(string email, string expectedMessage)
    {
        // Arrange
        var request = new ConfirmEmailResendRequest(Email: email);
        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }
}