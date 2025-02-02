using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;
using Bogus;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.Entities;

public class PacientePlanoMedicoTest
{
    private readonly Faker _faker;

    public PacientePlanoMedicoTest() => _faker = new Faker("pt_BR");

    [Fact]
    public void PacientePlanoMedico_ShouldBeValid_WhenAddNew_AllFieldsIsValid()
    {
        // Act & Assert
        FluentActions.Invoking(() => new PacientePlanoMedico(_faker.Finance.AccountName(), _faker.Finance.CreditCardNumber(), 1, 1))
            .Should().NotThrow();
    }

    [Fact]
    public void PacientePlanoMedico_ShouldBeValid_WhenUpdate_AllFieldsIsValid()
    {
        // Arrange
        var obj = new PacientePlanoMedico(_faker.Finance.AccountName(), _faker.Finance.CreditCardNumber(), 1, 1);
        // Act & Assert
        FluentActions.Invoking(() => obj.Update(_faker.Finance.AccountName(), _faker.Finance.CreditCardNumber()))
            .Should().NotThrow();
    }

    [Theory]
    [InlineData(null, "4456454465", 1, 1, "NomePlano não pode ser nulo ou vazio.")]
    [InlineData("", "4456454465", 1, 1, "NomePlano não pode ser nulo ou vazio.")]
    [InlineData("Teste", null, 1, 1, "NumCartao não pode ser nulo ou vazio.")]
    [InlineData("Teste", "", 1, 1, "NumCartao não pode ser nulo ou vazio.")]
    [InlineData("Teste", "4456454465", 0, 1, "PacienteId deve ser maior que zero.")]
    [InlineData("Teste", "4456454465", 1, 0, "ConvenioMedicoId deve ser maior que zero.")]
    public void PacientePlanoMedico_ShouldThrowException_WhenAddNew_FieldsAreInvalid(string nomePlano, string numCartao, int pacienteId, int convenioMedicoId, string expectedMessage)
    {
        var ex = Assert.Throws<EntityValidationException>(() => new PacientePlanoMedico(nomePlano, numCartao, pacienteId, convenioMedicoId));
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Theory]
    [InlineData(null, "4456454465", "NomePlano não pode ser nulo ou vazio.")]
    [InlineData("", "4456454465", "NomePlano não pode ser nulo ou vazio.")]
    [InlineData("Teste", null, "NumCartao não pode ser nulo ou vazio.")]
    [InlineData("Teste", "", "NumCartao não pode ser nulo ou vazio.")]
    public void PacientePlanoMedico_ShouldThrowException_WhenUpdate_FieldsAreInvalid(string nomePlano, string numCartao, string expectedMessage)
    {
        // Arrange
        var obj = new PacientePlanoMedico(_faker.Finance.AccountName(), _faker.Finance.CreditCardNumber(), 1, 1);
        // Act
        var ex = Assert.Throws<EntityValidationException>(() => obj.Update(nomePlano, numCartao));
        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Theory(DisplayName = "Deve alterar o status corretamente ao chamar ChangeStatus")]
    [InlineData(true)]
    [InlineData(false)]
    public void PacientePlanoMedico_ChangeStatus_ShouldChangeStatus_WhenCalledWithBooleanValue(bool status)
    {
        // Arrange
        var obj = new PacientePlanoMedico(_faker.Finance.AccountName(), _faker.Finance.CreditCardNumber(), 1, 1) { Ativo = !status };

        // Act
        obj.ChangeStatus(status);

        // Assert
        obj.Ativo.Should().Be(status);
    }
}