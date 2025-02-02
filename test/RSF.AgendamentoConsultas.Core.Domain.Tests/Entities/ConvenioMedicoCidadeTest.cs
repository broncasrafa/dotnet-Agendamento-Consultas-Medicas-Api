using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Exceptions;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Domain.Tests.Entities;

public class ConvenioMedicoCidadeTest
{
    [Fact(DisplayName = "Deve validar objeto com sucesso")]
    public void ConvenioMedicoCidade_ShouldBeValid_WhenAddNew_AllFieldsIsValid()
    {
        // Arrange
        var convenioMedico = new ConvenioMedicoCidade(1, 1, 1);

        // Act & Assert
        FluentActions.Invoking(() => convenioMedico).Should().NotThrow();

        convenioMedico.Should().NotBeNull();
    }

    [Fact(DisplayName = "Deve lançar exceção para ConvenioMedicoId inválido")]
    public void ConvenioMedicoCidade_ShouldThrowException_WhenConvenioMedicoIdIsInvalid()
    {
        // Arrange & Act
        var ex = Assert.Throws<EntityValidationException>(() =>
            new ConvenioMedicoCidade(convenioMedicoId: 0, cidadeId: 1, estadoId: 1));
        
        // Assert
        Assert.Equal("ConvenioMedicoId deve ser maior que zero.", ex.Message);
    }

    [Fact(DisplayName = "Deve lançar exceção para CidadeId inválido")]
    public void ConvenioMedicoCidade_ShouldThrowException_WhenCidadeIdIsInvalid()
    {
        // Arrange & Act
        var ex = Assert.Throws<EntityValidationException>(() =>
            new ConvenioMedicoCidade(convenioMedicoId: 1, cidadeId: 0, estadoId: 1));

        // Assert
        Assert.Equal("CidadeId deve ser maior que zero.", ex.Message);
    }

    [Fact(DisplayName = "Deve lançar exceção para EstadoId inválido")]
    public void ConvenioMedicoCidade_ShouldThrowException_WhenEstadoIdsInvalid()
    {
        // Arrange & Act
        var ex = Assert.Throws<EntityValidationException>(() =>
            new ConvenioMedicoCidade(convenioMedicoId: 1, cidadeId: 1, estadoId: 0));

        // Assert
        Assert.Equal("EstadoId deve ser maior que zero.", ex.Message);
    }
}