using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;
using Bogus;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.Entities;

public class ConvenioMedicoTest
{
    private readonly Faker _faker;

    public ConvenioMedicoTest() => _faker = new Faker(locale: "pt_BR");

    [Fact]
    public void ConvenioMedico_ShouldBeValid_WhenAddNew_AllFieldsIsValid()
    {
        // Arrange
        var convenioMedico = new ConvenioMedico(1, nome: _faker.Lorem.Text());

        // Act & Assert
        FluentActions.Invoking(() => convenioMedico).Should().NotThrow();

        convenioMedico.Should().NotBeNull();
    }

    [Fact]
    public void ConvenioMedico_ShouldThrowException_WhenAddNew_NomeIsNullOrEmpty()
    {
        // Act
        var ex = Assert.Throws<EntityValidationException>(() => new ConvenioMedico(nome: null));
        // Assert
        Assert.Equal("Nome não pode ser nulo ou vazio.", ex.Message);
    }

    [Fact]
    public void ConvenioMedico_ShouldBeValid_WhenUpdate_AllFieldsIsValid()
    {
        // Arrange
        var convenioMedico = new ConvenioMedico(1, _faker.Lorem.Text());

        // Act & Assert
        FluentActions.Invoking(() => convenioMedico.Update(_faker.Lorem.Text())).Should().NotThrow();

        convenioMedico.Should().NotBeNull();
    }

    [Fact]
    public void ConvenioMedico_ShouldThrowException_WhenUpdate_NomeIsNullOrEmpty()
    {
        // Arrange
        var convenioMedico = new ConvenioMedico(nome: _faker.Commerce.ProductName());
        // Act
        var ex = Assert.Throws<EntityValidationException>(() => convenioMedico.Update(nome: null));
        // Assert
        Assert.Equal("Nome não pode ser nulo ou vazio.", ex.Message);
    }
}