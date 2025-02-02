using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;
using Bogus;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.Entities;

public class EspecialistaLocalAtendimentoTest
{
    private readonly Faker _faker;

    public EspecialistaLocalAtendimentoTest() => _faker = new Faker("pt_BR");

    [Fact]
    public void EspecialistaLocalAtendimento_ShouldBeValid_WhenAddNew_AllFieldsIsValid()
    {
        // Arrange
        var obj = new EspecialistaLocalAtendimento(
            especialistaId: 1,
            nome: _faker.Company.CompanyName(),
            logradouro: "Avenida dos Testes",
            complemento: "Complemento Teste",
            bairro: "Bairro Teste",
            cep: "09999000",
            cidade: "São Paulo",
            estado: "SP",
            preco: _faker.Random.Decimal(100, 2000),
            tipoAtendimento: _faker.Name.FirstName(),
            telefone: "11965890023",
            whatsapp: "11965890023"
        );

        // Act & Assert
        FluentActions.Invoking(() => obj).Should().NotThrow();

        obj.Should().NotBeNull();
    }

    [Theory]
    [InlineData(0, "11965890023", "11965890023", 150.90, "09999000", "SP", "EspecialistaId deve ser maior que zero.")]
    [InlineData(1, "invalid-phone", "11965890023", 150.90, "09999000", "SP", "Telefone deve conter apenas números.")]
    [InlineData(1, "12345", "11965890023", 150.90, "09999000", "SP", "Telefone deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    [InlineData(1, "123456789012", "11965890023", 150.90, "09999000", "SP", "Telefone deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    [InlineData(1, "11965890023", "invalid-phone", 150.90, "09999000", "SP", "Whatsapp deve conter apenas números.")]
    [InlineData(1, "11965890023", "12345", 150.90, "09999000", "SP", "Whatsapp deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    [InlineData(1, "11965890023", "123456789012", 150.90, "09999000", "SP", "Whatsapp deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    [InlineData(1, "11965890023", "11965890023", 0, "09999000", "SP", "Preco deve ser maior que zero.")]
    [InlineData(1, "11965890023", "11965890023", -150.90, "09999000", "SP", "Preco deve ser maior que zero.")]
    [InlineData(1, "11965890023", "11965890023", 150.90, "123A", "SP", "CEP deve conter apenas números.")]
    [InlineData(1, "11965890023", "11965890023", 150.90, "12-34", "SP", "CEP deve conter exatamente 8 digitos.")]
    [InlineData(1, "11965890023", "11965890023", 150.90, "abc123", "SP", "CEP deve conter apenas números.")]
    [InlineData(1, "11965890023", "11965890023", 150.90, "099780012", "SP", "CEP deve conter exatamente 8 digitos.")]
    [InlineData(1, "11965890023", "11965890023", 150.90, "0780012", "SP", "CEP deve conter exatamente 8 digitos.")]
    [InlineData(1, "11965890023", "11965890023", 150.90, "09999000", "", "Estado deve ter 2 caracteres.")]
    [InlineData(1, "11965890023", "11965890023", 150.90, "09999000", "S", "Estado deve ter 2 caracteres.")]
    [InlineData(1, "11965890023", "11965890023", 150.90, "09999000", "SSP", "Estado deve ter 2 caracteres.")]
    [InlineData(1, "11965890023", "11965890023", 150.90, "09999000", "sp", "Estado deve conter apenas letras maiúsculas.")]
    [InlineData(1, "11965890023", "11965890023", 150.90, "09999000", "Sp", "Estado deve conter apenas letras maiúsculas.")]
    [InlineData(1, "11965890023", "11965890023", 150.90, "09999000", "sP", "Estado deve conter apenas letras maiúsculas.")]
    [InlineData(1, "11965890023", "11965890023", 150.90, "09999000", "XP", "Estado inválido. Valores válidos: 'AC, AL, AP, AM, BA, CE, DF, ES, GO, MA, MT, MS, MG, PA, PB, PR, PE, PI, RJ, RN, RS, RO, RR, SC, SP, SE, TO'")]
    public void EspecialistaLocalAtendimento_ShouldThrowException_WhenAddNew_FieldsAreInvalid(int especialistaId, string telefone, string whatsapp, decimal preco, string cep, string estado, string expectedMessage)
    {
        // Arrange & Act
        var ex = Assert.Throws<EntityValidationException>(() => new EspecialistaLocalAtendimento(
            especialistaId: especialistaId,
            nome: _faker.Company.CompanyName(),
            logradouro: "Avenida dos Testes",
            complemento: "Complemento Teste",
            bairro: "Bairro Teste",
            cep: cep,
            cidade: "São Paulo",
            estado: estado,
            preco: preco,
            tipoAtendimento: _faker.Name.FirstName(),
            telefone: telefone,
            whatsapp: whatsapp
        ));
        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }



    [Fact]
    public void EspecialistaLocalAtendimento_ShouldBeValid_WhenUpdate_AllFieldsIsValid()
    {
        // Arrange
        var obj = new EspecialistaLocalAtendimento(
            especialistaId: 1,
            nome: _faker.Company.CompanyName(),
            logradouro: "Avenida dos Testes",
            complemento: "Complemento Teste",
            bairro: "Bairro Teste",
            cep: "09999000",
            cidade: "São Paulo",
            estado: "SP",
            preco: _faker.Random.Decimal(100, 2000),
            tipoAtendimento: _faker.Name.FirstName(),
            telefone: "11965890023",
            whatsapp: "11965890023"
        );

        // Act & Assert
        FluentActions.Invoking(() => obj.Update(
            nome: _faker.Company.CompanyName(),
            logradouro: "Rua dos Testes",
            complemento: "Complemento Teste 2",
            bairro: "Bairro dos Teste",
            cep: "09999000",
            cidade: "São Paulo",
            estado: "SP",
            preco: _faker.Random.Decimal(100, 2000),
            tipoAtendimento: _faker.Name.FirstName(),
            telefone: "11965890024",
            whatsapp: "11965890024")).Should().NotThrow();

        obj.Should().NotBeNull();
    }

    [Theory]
    [InlineData("invalid-phone", "11965890023", 150.90, "09999000", "SP", "Telefone deve conter apenas números.")]
    [InlineData("12345", "11965890023", 150.90, "09999000", "SP", "Telefone deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    [InlineData("123456789012", "11965890023", 150.90, "09999000", "SP", "Telefone deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    [InlineData("11965890023", "invalid-phone", 150.90, "09999000", "SP", "Whatsapp deve conter apenas números.")]
    [InlineData("11965890023", "12345", 150.90, "09999000", "SP", "Whatsapp deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    [InlineData("11965890023", "123456789012", 150.90, "09999000", "SP", "Whatsapp deve conter 10 (fixo) ou 11 (celular) dígitos.")]
    [InlineData("11965890023", "11965890023", 0, "09999000", "SP", "Preco deve ser maior que zero.")]
    [InlineData("11965890023", "11965890023", -150.90, "09999000", "SP", "Preco deve ser maior que zero.")]
    [InlineData("11965890023", "11965890023", 150.90, "123A", "SP", "CEP deve conter apenas números.")]
    [InlineData("11965890023", "11965890023", 150.90, "12-34", "SP", "CEP deve conter exatamente 8 digitos.")]
    [InlineData("11965890023", "11965890023", 150.90, "abc123", "SP", "CEP deve conter apenas números.")]
    [InlineData("11965890023", "11965890023", 150.90, "099780012", "SP", "CEP deve conter exatamente 8 digitos.")]
    [InlineData("11965890023", "11965890023", 150.90, "0780012", "SP", "CEP deve conter exatamente 8 digitos.")]
    [InlineData("11965890023", "11965890023", 150.90, "09999000", "", "Estado deve ter 2 caracteres.")]
    [InlineData("11965890023", "11965890023", 150.90, "09999000", "S", "Estado deve ter 2 caracteres.")]
    [InlineData("11965890023", "11965890023", 150.90, "09999000", "SSP", "Estado deve ter 2 caracteres.")]
    [InlineData("11965890023", "11965890023", 150.90, "09999000", "sp", "Estado deve conter apenas letras maiúsculas.")]
    [InlineData("11965890023", "11965890023", 150.90, "09999000", "Sp", "Estado deve conter apenas letras maiúsculas.")]
    [InlineData("11965890023", "11965890023", 150.90, "09999000", "sP", "Estado deve conter apenas letras maiúsculas.")]
    [InlineData("11965890023", "11965890023", 150.90, "09999000", "XP", "Estado inválido. Valores válidos: 'AC, AL, AP, AM, BA, CE, DF, ES, GO, MA, MT, MS, MG, PA, PB, PR, PE, PI, RJ, RN, RS, RO, RR, SC, SP, SE, TO'")]
    public void EspecialistaLocalAtendimento_ShouldThrowException_WhenUpdate_FieldsAreInvalid(string telefone, string whatsapp, decimal preco, string cep, string estado, string expectedMessage)
    {
        // Arrange
        var obj = new EspecialistaLocalAtendimento(
            especialistaId: 1,
            nome: _faker.Company.CompanyName(),
            logradouro: "Avenida dos Testes",
            complemento: "Complemento Teste",
            bairro: "Bairro Teste",
            cep: "09999000",
            cidade: "São Paulo",
            estado: "SP",
            preco: _faker.Random.Decimal(100, 2000),
            tipoAtendimento: _faker.Name.FirstName(),
            telefone: "11965890023",
            whatsapp: "11965890023"
        );

        // Act
        var ex = Assert.Throws<EntityValidationException>(() => obj.Update(
            nome: _faker.Company.CompanyName(),
            logradouro: "Avenida dos Testes",
            complemento: "Complemento Teste",
            bairro: "Bairro Teste",
            cep: cep,
            cidade: "São Paulo",
            estado: estado,
            preco: preco,
            tipoAtendimento: _faker.Name.FirstName(),
            telefone: telefone,
            whatsapp: whatsapp
        ));

        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }

}

