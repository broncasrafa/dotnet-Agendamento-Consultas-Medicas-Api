using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Application.Features.Avaliacao.Query.GetAvaliacaoById;
using Moq;
using FluentAssertions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using System.Linq.Expressions;
using RSF.AgendamentoConsultas.Core.Application.Tests.Base;


namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Avaliacao.Query.GetAvaliacaoById;

public class SelectAvaliacaoByIdRequestHandlerTest : IClassFixture<BaseFixture>
{
    private readonly BaseFixture _fixture;
    private readonly Mock<IBaseRepository<EspecialistaAvaliacao>> _especialistaAvaliacaoRepositoryMock;
    private readonly SelectAvaliacaoByIdRequestHandler _handler;

    public SelectAvaliacaoByIdRequestHandlerTest(BaseFixture fixture)
    {
        _fixture = fixture;
        _especialistaAvaliacaoRepositoryMock = new();
        _handler = new(_especialistaAvaliacaoRepositoryMock.Object);
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Avaliação não encontrado")]
    public async Task Handle_ShouldThrowException_When_Avaliacao_NotExists()
    {
        // Arrange
        var request = new SelectAvaliacaoByIdRequest(1);
        _especialistaAvaliacaoRepositoryMock.Setup(x => x.GetByFilterAsync(It.IsAny<Expression<Func<EspecialistaAvaliacao, bool>>>()));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Avaliação com o ID: '{request.Id}' não encontrada");
    }

    [Fact(DisplayName = "Deve retornar a avaliação com sucesso")]
    public async Task Handle_ShouldReturn_When_RequestIsValid()
    {
        // Arrange
        var request = new SelectAvaliacaoByIdRequest(1);
        var avaliacao = new EspecialistaAvaliacao(1, 1, 1, _fixture.Faker.Lorem.Text(), 5, 1)
        {
            Paciente = _fixture.Paciente,
            Especialista = _fixture.Especialista,
        };
        _especialistaAvaliacaoRepositoryMock.Setup(x => x.GetByFilterAsync(
                It.IsAny<Expression<Func<EspecialistaAvaliacao, bool>>>(),
                It.IsAny<Expression<Func<EspecialistaAvaliacao, object>>[]>()))
            .ReturnsAsync(avaliacao);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Exception.Should().BeNull();
        result.Value.Should().NotBeNull();
    }
}