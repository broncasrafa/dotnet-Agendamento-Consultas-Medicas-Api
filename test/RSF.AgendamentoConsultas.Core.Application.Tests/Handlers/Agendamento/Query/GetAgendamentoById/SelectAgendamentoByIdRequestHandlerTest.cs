using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Query.GetAgendamentoById;
using RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Agendamento.Fixture;
using Moq;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Agendamento.Query.GetAgendamentoById;

[Collection(nameof(AgendamentoConsultaTestFixture))]
public class SelectAgendamentoByIdRequestHandlerTest
{
    private readonly AgendamentoConsultaTestFixture _fixture;
    private readonly Mock<IAgendamentoConsultaRepository> _agendamentoConsultaRepositoryMock;
    private readonly SelectAgendamentoByIdRequestHandler _handler;
    private readonly SelectAgendamentoByIdRequest _request;

    public SelectAgendamentoByIdRequestHandlerTest(AgendamentoConsultaTestFixture fixture)
    {
        _fixture = fixture;
        _agendamentoConsultaRepositoryMock = new Mock<IAgendamentoConsultaRepository>();
        _handler = new SelectAgendamentoByIdRequestHandler(_agendamentoConsultaRepositoryMock.Object);
        _request = new SelectAgendamentoByIdRequest(1);
    }

    [Fact]
    public async Task Handle_ShouldGetData_When_RequestIsValid()
    {
        // Arrange
        var agendamentoMock = _fixture.GetAgendamentoConsulta();

        _agendamentoConsultaRepositoryMock.Setup(c => c.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(agendamentoMock);

        // Act
        var result = await _handler.Handle(_request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Exception.Should().BeNull();

        _agendamentoConsultaRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowsException_When_AgendamentoIsNotFound()
    {
        // Arrange
        _agendamentoConsultaRepositoryMock.Setup(c => c.GetByIdAsync(It.IsAny<int>()));

        // Act
        Func<Task> act = async () => await _handler.Handle(_request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Agendamento com o ID: '{_request.Id}' não encontrado");

        _agendamentoConsultaRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }
}