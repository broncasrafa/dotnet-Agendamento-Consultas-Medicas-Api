﻿using RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.CancelAgendamentoByEspecialista;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Agendamento.Command.CancelAgendamentoByEspecialista;

public class CancelAgendamentoByEspecialistaRequestValidatorTest
{
    private readonly CancelAgendamentoByEspecialistaRequestValidator _validator;

    public CancelAgendamentoByEspecialistaRequestValidatorTest()
    {
        _validator = new CancelAgendamentoByEspecialistaRequestValidator();
    }


    [Fact(DisplayName = "Deve validar os dados de entrada com resultado de sucesso")]
    public void Validate_ShouldBeValid_When_AllInputs_IsValid()
    {
        // Arrange
        var request = new CancelAgendamentoByEspecialistaRequest(
            AgendamentoId: 1,
            EspecialistaId: 1);

        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }


    [Theory(DisplayName = "Deve retornar erro ao validar os identificadores (IDs)")]
    [InlineData(0, "O ID do Agendamento deve ser maior que 0")]
    [InlineData(-1, "O ID do Agendamento deve ser maior que 0")]
    [InlineData(0, "O ID do Especialista deve ser maior que 0")]
    [InlineData(-1, "O ID do Especialista deve ser maior que 0")]
    public void Validate_ShouldHaveError_When_Ids_IsInvalid(int id, string expectedMessage)
    {
        // Arrange
        var request = new CancelAgendamentoByEspecialistaRequest(
            AgendamentoId: id,
            EspecialistaId: id);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }
}