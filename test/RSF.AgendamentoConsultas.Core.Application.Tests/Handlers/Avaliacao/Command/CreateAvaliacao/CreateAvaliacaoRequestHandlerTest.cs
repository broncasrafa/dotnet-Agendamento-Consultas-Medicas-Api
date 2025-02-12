using RSF.AgendamentoConsultas.Core.Application.Tests.Base;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Application.Features.Avaliacao.Command.CreateAvaliacao;
using Moq;
using FluentAssertions;
using RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.ConfirmAgendamentoByPaciente;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using Bogus;


namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Avaliacao.Command.CreateAvaliacao;

public class CreateAvaliacaoRequestHandlerTest : IClassFixture<BaseFixture>
{
    private readonly BaseFixture _fixture;
    private readonly Mock<IPacienteRepository> _pacienteRepositoryMock;
    private readonly Mock<IEspecialistaRepository> _especialistaRepositoryMock;
    private readonly Mock<IBaseRepository<EspecialistaAvaliacao>> _especialistaAvaliacaoRepositoryMock;
    private readonly Mock<IBaseRepository<Tags>> _tagsRepositoryMock;
    private readonly CreateAvaliacaoRequestHandler _handler;
    private readonly CreateAvaliacaoRequest _request;

    public CreateAvaliacaoRequestHandlerTest(BaseFixture fixture)
    {
        _fixture = fixture;
        _pacienteRepositoryMock = new();
        _especialistaRepositoryMock = new();
        _especialistaAvaliacaoRepositoryMock = new();
        _tagsRepositoryMock = new();
        _handler = new(_pacienteRepositoryMock.Object, _especialistaRepositoryMock.Object, _especialistaAvaliacaoRepositoryMock.Object, _tagsRepositoryMock.Object);
        _request = new (
            EspecialistaId: 1,
            PacienteId: 1,
            TagId: 1,
            Feedback: _fixture.Faker.Lorem.Text(),
            Score: 1);
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Especialista não encontrado")]
    public async Task Handle_ShouldThrowException_When_EspecialistaId_NotExists()
    {
        // Arrange
        _especialistaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()));

        // Act
        Func<Task> act = async () => await _handler.Handle(_request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Especialista com o ID: '{_request.EspecialistaId}' não encontrado");
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Paciente não encontrado")]
    public async Task Handle_ShouldThrowException_When_PacienteId_NotExists()
    {
        // Arrange
        var especialista = _fixture.Especialista;
        _especialistaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(especialista);
        _pacienteRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()));

        // Act
        Func<Task> act = async () => await _handler.Handle(_request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Paciente com o ID: '{_request.PacienteId}' não foi encontrado");
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Tag não encontrado")]
    public async Task Handle_ShouldThrowException_When_TagId_NotExists()
    {
        // Arrange
        var especialista = _fixture.Especialista;
        var paciente = _fixture.Paciente;
        _especialistaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(especialista);
        _pacienteRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(paciente);
        _tagsRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()));

        // Act
        Func<Task> act = async () => await _handler.Handle(_request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Tag com o ID: '{_request.TagId!.Value}' não foi encontrada");
    }

    [Fact(DisplayName = "Deve confirmar o agendamento com sucesso")]
    public async Task Handle_ShouldConfirm_When_RequestIsValid()
    {
        // Arrange
        var especialista = _fixture.Especialista;
        var paciente = _fixture.Paciente;
        var tag = new Tags(1, "Teste");
        _especialistaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(especialista);
        _pacienteRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(paciente);
        _tagsRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(tag);
        _especialistaAvaliacaoRepositoryMock.Setup(x => x.AddAsync(It.IsAny<EspecialistaAvaliacao>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(_request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Exception.Should().BeNull();
        result.Value.Should().BeTrue();
    }
}