using RSF.AgendamentoConsultas.Core.Application.Features.Avaliacao.Command.CreateAvaliacao;
using Bogus;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Avaliacao.Command.CreateAvaliacao;

public class CreateAvaliacaoRequestValidatorTest
{
    private readonly CreateAvaliacaoRequestValidator _validator;
    private readonly Faker _faker;
    public CreateAvaliacaoRequestValidatorTest()
    {
        _validator = new();
        _faker = new(locale: "pt_BR");
    }


    [Fact(DisplayName = "Deve validar os dados de entrada com resultado de sucesso")]
    public void Validate_ShouldBeValid_When_AllInputs_IsValid()
    {
        // Arrange
        var request = new CreateAvaliacaoRequest(
            EspecialistaId: 1, 
            PacienteId: 1, 
            TagId: 1, 
            Feedback: _faker.Lorem.Text(), 
            Score: 1);

        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory(DisplayName = "Deve retornar erro ao validar os identificadores (IDs)")]
    [InlineData(0, "O ID do Paciente deve ser maior que 0")]
    [InlineData(-1, "O ID do Paciente deve ser maior que 0")]
    [InlineData(0, "O ID do Especialista deve ser maior que 0")]
    [InlineData(-1, "O ID do Especialista deve ser maior que 0")]
    [InlineData(0, "O ID da Tag deve ser maior que 0")]
    [InlineData(-1, "O ID da Tag deve ser maior que 0")]
    public void Validate_ShouldHaveError_When_Ids_IsInvalid(int id, string expectedMessage)
    {
        // Arrange
        var request = new CreateAvaliacaoRequest(
            EspecialistaId: id,
            PacienteId: id,
            TagId: id,
            Feedback: _faker.Lorem.Text(),
            Score: 1);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }


    [Theory(DisplayName = "Deve retornar erro ao validar o Feedback")]
    [InlineData(null, "O Feedback da avaliação é obrigatório, não deve ser nulo ou vazio")]
    [InlineData("", "O Feedback da avaliação é obrigatório, não deve ser nulo ou vazio")]
    [InlineData("Test", "O Feedback da avaliação deve ter pelo menos 5 caracteres")]
    public void Validate_ShouldHaveError_When_Feedback_IsInvalid(string Feedback, string expectedMessage)
    {
        // Arrange
        var request = new CreateAvaliacaoRequest(
            EspecialistaId: 1,
            PacienteId: 1,
            TagId: 1,
            Feedback: Feedback,
            Score: 1);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }


    [Theory(DisplayName = "Deve retornar erro ao validar o Score")]
    [InlineData(0, "O Score da avaliação deve ser um valor entre 1 e 5")]
    [InlineData(-1, "O Score da avaliação deve ser um valor entre 1 e 5")]
    [InlineData(10, "O Score da avaliação deve ser um valor entre 1 e 5")]
    public void Validate_ShouldHaveError_When_Score_IsInvalid(int Score, string expectedMessage)
    {
        // Arrange
        var request = new CreateAvaliacaoRequest(
            EspecialistaId: 1,
            PacienteId: 1,
            TagId: 1,
            Feedback: _faker.Lorem.Text(),
            Score: Score);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().ContainSingle(e => e.ErrorMessage == expectedMessage);
    }
}