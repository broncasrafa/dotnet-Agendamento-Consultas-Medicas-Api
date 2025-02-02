using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.Entities;

public class EspecialistaConvenioMedicoTest
{

    [Fact]
    public void EspecialistaConvenioMedico_ShouldBeValid_WhenAddNew_AllFieldsIsValid()
    {
        // Arrange
        var obj = new EspecialistaConvenioMedico(1, 1);

        // Act & Assert
        FluentActions.Invoking(() => obj).Should().NotThrow();

        obj.Should().NotBeNull();
    }

    [Theory]
    [InlineData(0, 1, "EspecialistaId deve ser maior que zero.")]
    [InlineData(1, 0, "ConvenioMedicoId deve ser maior que zero.")]
    public void EspecialistaConvenioMedico_ShouldNotBeValid_WhenAddNew_FieldsAreInvalid(int especialistaId, int convenioMedicoId, string expectedMessage)
    {
        var ex = Assert.Throws<EntityValidationException>(() => new EspecialistaConvenioMedico(especialistaId, convenioMedicoId));
        Assert.Equal(expectedMessage, ex.Message);
    }



    [Fact]
    public void EspecialistaConvenioMedico_ShouldBeValid_WhenUpdate_AllFieldsIsValid()
    {
        // Arrange
        var obj = new EspecialistaConvenioMedico(1, 1);

        // Act & Assert
        FluentActions.Invoking(() => obj.Update(2)).Should().NotThrow();

        obj.Should().NotBeNull();
    }

    [Fact]
    public void EspecialistaConvenioMedico_ShouldNotBeValid_WhenUpdate_FieldsAreInvalid()
    {
        var obj = new EspecialistaConvenioMedico(1, 1);
        var ex = Assert.Throws<EntityValidationException>(() => obj.Update(0));
        Assert.Equal("ConvenioMedicoId deve ser maior que zero.", ex.Message);
    }
}