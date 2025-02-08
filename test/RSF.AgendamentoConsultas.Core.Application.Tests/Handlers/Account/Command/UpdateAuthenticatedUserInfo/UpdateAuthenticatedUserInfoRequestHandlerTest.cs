using System.Security.Claims;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.UpdateAuthenticatedUserInfo;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Application.Tests.Base;
using RSF.AgendamentoConsultas.Core.Application.Tests.Base.Extensions;
using Moq;
using FluentAssertions;
using Bogus.Extensions.Brazil;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Account.Command.UpdateAuthenticatedUserInfo;

public class UpdateAuthenticatedUserInfoRequestHandlerTest : IClassFixture<BaseFixture>
{
    private readonly BaseFixture _fixture;
    private readonly Mock<IEventBus> _eventBusMock;
    private readonly Mock<IAccountManagerService> _accountManagerServiceMock;
    private readonly Mock<IPacienteRepository> _pacienteRepositoryMock;
    private readonly Mock<IEspecialistaRepository> _especialistaRepositoryMock;
    private readonly UpdateAuthenticatedUserInfoRequestHandler _handler;
    private readonly UpdateAuthenticatedUserInfoRequest _request;

    public UpdateAuthenticatedUserInfoRequestHandlerTest(BaseFixture fixture)
    {
        _fixture = fixture;
        _accountManagerServiceMock = new Mock<IAccountManagerService>();
        _especialistaRepositoryMock = new Mock<IEspecialistaRepository>();
        _pacienteRepositoryMock = new Mock<IPacienteRepository>();
        _eventBusMock = new Mock<IEventBus>();

        _handler = new UpdateAuthenticatedUserInfoRequestHandler(
            _fixture.HttpContextAccessorMockAdmin.Object,
            _eventBusMock.Object,
            _fixture.ConfigurationMock.Object,
            _accountManagerServiceMock.Object,
            _pacienteRepositoryMock.Object,
            _especialistaRepositoryMock.Object);

        _request = new UpdateAuthenticatedUserInfoRequest(
                NomeCompleto: _fixture.Faker.Person.FullName,
                Telefone: _fixture.Faker.Person.CustomCellPhoneBR(),
                Email: _fixture.Faker.Person.Email,
                Username: _fixture.Faker.Person.UserName);
    }


    [Fact(DisplayName = "Deve lançar exceção UnauthorizedRequestException quando usuário não está autenticado")]
    public async Task Handle_ShouldThrowException_When_UserIsNotAuthenticated()
    {
        // Arrange
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext());

        var handler = new UpdateAuthenticatedUserInfoRequestHandler(
            httpContextAccessorMock.Object,
            _eventBusMock.Object,
            _fixture.ConfigurationMock.Object,
            _accountManagerServiceMock.Object,
            _pacienteRepositoryMock.Object,
            _especialistaRepositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(_request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedRequestException>()
            .WithMessage("Usuário não está autenticado na plataforma");

        _accountManagerServiceMock.Verify(c => c.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Never);
        _accountManagerServiceMock.Verify(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Never);
        _accountManagerServiceMock.Verify(c => c.UpdateUserAsync(It.IsAny<ApplicationUser>()), Times.Never);
        _accountManagerServiceMock.Verify(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()), Times.Never);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<EmailConfirmationCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Never);
        _pacienteRepositoryMock.Verify(x => x.GetByFilterAsync(It.IsAny<Expression<Func<Paciente, bool>>>(), It.IsAny<Expression<Func<Paciente, object>>[]>()), Times.Never);
        _pacienteRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Paciente>()), Times.Never);
        _especialistaRepositoryMock.Verify(x => x.GetByFilterAsync(It.IsAny<Expression<Func<Especialista, bool>>>(), It.IsAny<Expression<Func<Especialista, object>>[]>()), Times.Never);
        _especialistaRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Especialista>()), Times.Never);
    }


    [Fact(DisplayName = "Deve lançar exceção NotFoundException quando usuário não foi encontrado")]
    public async Task Handle_ShouldThrowException_When_UserIsNotFound()
    {
        // Arrange
        _accountManagerServiceMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()));

        // Act
        var act = async () => await _handler.Handle(_request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Usuário não está autenticado na plataforma");

        _accountManagerServiceMock.Verify(c => c.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Never);
        _accountManagerServiceMock.Verify(c => c.UpdateUserAsync(It.IsAny<ApplicationUser>()), Times.Never);
        _accountManagerServiceMock.Verify(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()), Times.Never);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<EmailConfirmationCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Never);
        _pacienteRepositoryMock.Verify(x => x.GetByFilterAsync(It.IsAny<Expression<Func<Paciente, bool>>>(), It.IsAny<Expression<Func<Paciente, object>>[]>()), Times.Never);
        _pacienteRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Paciente>()), Times.Never);
        _especialistaRepositoryMock.Verify(x => x.GetByFilterAsync(It.IsAny<Expression<Func<Especialista, bool>>>(), It.IsAny<Expression<Func<Especialista, object>>[]>()), Times.Never);
        _especialistaRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Especialista>()), Times.Never);
    }


    [Fact(DisplayName = "Deve lançar exceção AlreadyExistsException caso o e-mail precisa ser verificado e o email novo já exista")]
    public async Task Handle_ShouldThrowException_When_UserEmailAlreadyExists()
    {
        // Arrange
        var authenticatedUser = new ApplicationUser(
            nomeCompleto: _fixture.Faker.Person.FullName,
            username: $"TestUser1",
            documento: _fixture.Faker.Person.Cpf(),
            email: $"TestUser1@teste.com",
            genero: _fixture.Faker.CustomGender(),
            telefone: _fixture.Faker.Person.CustomCellPhoneBR())
        { Id = Guid.NewGuid().ToString(), IsActive = true };

        var request = new UpdateAuthenticatedUserInfoRequest(
                NomeCompleto: _fixture.Faker.Person.FullName,
                Telefone: _fixture.Faker.Person.CustomCellPhoneBR(),
                Email: _fixture.Faker.Person.Email,
                Username: _fixture.Faker.Person.UserName);

        _accountManagerServiceMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(authenticatedUser);
        _accountManagerServiceMock.Setup(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>())).ReturnsAsync(authenticatedUser);
        //_accountManagerServiceMock.Setup(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>())).Returns(Task.FromResult<ApplicationUser?>(null));


        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<AlreadyExistsException>()
            .WithMessage("E-mail já cadastrado na plataforma");

        _accountManagerServiceMock.Verify(c => c.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Once);
        _accountManagerServiceMock.Verify(c => c.UpdateUserAsync(It.IsAny<ApplicationUser>()), Times.Never);
        _accountManagerServiceMock.Verify(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()), Times.Never);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<EmailConfirmationCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Never);
        _pacienteRepositoryMock.Verify(x => x.GetByFilterAsync(It.IsAny<Expression<Func<Paciente, bool>>>(), It.IsAny<Expression<Func<Paciente, object>>[]>()), Times.Never);
        _pacienteRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Paciente>()), Times.Never);
        _especialistaRepositoryMock.Verify(x => x.GetByFilterAsync(It.IsAny<Expression<Func<Especialista, bool>>>(), It.IsAny<Expression<Func<Especialista, object>>[]>()), Times.Never);
        _especialistaRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Especialista>()), Times.Never);
    }


    [Fact(DisplayName = "Deve lançar exceção AlreadyExistsException caso o username precisa ser verificado e o username novo já exista")]
    public async Task Handle_ShouldThrowException_When_UserNameAlreadyExists()
    {
        // Arrange
        var authenticatedUser = new ApplicationUser(
            nomeCompleto: _fixture.Faker.Person.FullName,
            username: $"TestUser",
            documento: _fixture.Faker.Person.Cpf(),
            email: "TestUser@teste.com",
            genero: _fixture.Faker.CustomGender(),
            telefone: _fixture.Faker.Person.CustomCellPhoneBR())
            { Id = Guid.NewGuid().ToString(), IsActive = true };

        var request = new UpdateAuthenticatedUserInfoRequest(
                NomeCompleto: _fixture.Faker.Person.FullName,
                Telefone: _fixture.Faker.Person.CustomCellPhoneBR(),
                Email: "TestUser@teste.com",
                Username: _fixture.Faker.Person.UserName);

        _accountManagerServiceMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(authenticatedUser);
        _accountManagerServiceMock.Setup(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>())).Returns(Task.FromResult<ApplicationUser?>(null));
        _accountManagerServiceMock.Setup(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>())).ReturnsAsync(authenticatedUser);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<AlreadyExistsException>()
            .WithMessage("Nome de usuário já cadastrado na plataforma");

        _accountManagerServiceMock.Verify(c => c.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Once);
        _accountManagerServiceMock.Verify(c => c.UpdateUserAsync(It.IsAny<ApplicationUser>()), Times.Never);
        _accountManagerServiceMock.Verify(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()), Times.Never);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<EmailConfirmationCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Never);
        _pacienteRepositoryMock.Verify(x => x.GetByFilterAsync(It.IsAny<Expression<Func<Paciente, bool>>>(), It.IsAny<Expression<Func<Paciente, object>>[]>()), Times.Never);
        _pacienteRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Paciente>()), Times.Never);
        _especialistaRepositoryMock.Verify(x => x.GetByFilterAsync(It.IsAny<Expression<Func<Especialista, bool>>>(), It.IsAny<Expression<Func<Especialista, object>>[]>()), Times.Never);
        _especialistaRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Especialista>()), Times.Never);
    }


    [Fact(DisplayName = "Deve retornar erro se o update falhar")]
    public async Task Handle_ShouldThrowException_When_UserUpdateItFails()
    {
        // Arrange
        var authenticatedUser = _fixture.UserAdmin;
        _accountManagerServiceMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(authenticatedUser);
        _accountManagerServiceMock.SetupSequence(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
            .ReturnsAsync((ApplicationUser)null!)
            .ReturnsAsync((ApplicationUser)null!);
        _accountManagerServiceMock.Setup(x => x.UpdateUserAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(_request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Exception.Should().BeOfType<OperationErrorException>()
            .Which.Message.Should().Be("Falha ao atualizar os dados do usuário.");

        _accountManagerServiceMock.Verify(c => c.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Exactly(2));
        _accountManagerServiceMock.Verify(c => c.UpdateUserAsync(It.IsAny<ApplicationUser>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()), Times.Never);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<EmailConfirmationCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Never);
        _pacienteRepositoryMock.Verify(x => x.GetByFilterAsync(It.IsAny<Expression<Func<Paciente, bool>>>(), It.IsAny<Expression<Func<Paciente, object>>[]>()), Times.Never);
        _pacienteRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Paciente>()), Times.Never);
        _especialistaRepositoryMock.Verify(x => x.GetByFilterAsync(It.IsAny<Expression<Func<Especialista, bool>>>(), It.IsAny<Expression<Func<Especialista, object>>[]>()), Times.Never);
        _especialistaRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Especialista>()), Times.Never);
    }



    [Fact(DisplayName = "Deve atualizar os dados do usuário ADMIN e publicar evento com sucesso")]
    public async Task Handle_ShouldUpdateAdminDataAndPublishEvent_WhenRequestIsValid()
    {
        // Arrange
        var authenticatedUser = new ApplicationUser(
            nomeCompleto: _fixture.Faker.Person.FullName,
            username: $"TestUser",
            documento: _fixture.Faker.Person.Cpf(),
            email: "TestUser@teste.com",
            genero: _fixture.Faker.CustomGender(),
            telefone: _fixture.Faker.Person.CustomCellPhoneBR())
        { Id = Guid.NewGuid().ToString(), IsActive = true };

        _accountManagerServiceMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(authenticatedUser);
        _accountManagerServiceMock.SetupSequence(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
            .ReturnsAsync((ApplicationUser)null!)
            .ReturnsAsync((ApplicationUser)null!);
        _accountManagerServiceMock.Setup(x => x.UpdateUserAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(true);
        _accountManagerServiceMock.Setup(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>())).ReturnsAsync("NyeVfu3nZTZizATtjZtSbTEU");
        _fixture.MockRabbitMqQueueConfiguration("RabbitMQ:EmailConfirmationQueueName", "ha.queue.mock_queue_name");

        // Act
        var result = await _handler.Handle(_request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Exception.Should().BeNull();
        
        _accountManagerServiceMock.Verify(c => c.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Exactly(2));
        _accountManagerServiceMock.Verify(c => c.UpdateUserAsync(It.IsAny<ApplicationUser>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()), Times.Once);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<EmailConfirmationCreatedEvent>(), "ha.queue.mock_queue_name", ""), Times.Once);
        _pacienteRepositoryMock.Verify(x => x.GetByFilterAsync(It.IsAny<Expression<Func<Paciente, bool>>>(), It.IsAny<Expression<Func<Paciente, object>>[]>()), Times.Never);
        _pacienteRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Paciente>()), Times.Never);
        _especialistaRepositoryMock.Verify(x => x.GetByFilterAsync(It.IsAny<Expression<Func<Especialista, bool>>>(), It.IsAny<Expression<Func<Especialista, object>>[]>()), Times.Never);
        _especialistaRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Especialista>()), Times.Never);
    }

    [Fact(DisplayName = "Deve atualizar os dados do usuário PACIENTE e publicar evento com sucesso")]
    public async Task Handle_ShouldUpdatePacienteDataAndPublishEvent_WhenRequestIsValid()
    {
        // Arrange
        var authenticatedUser = _fixture.UserPaciente;
        var pacienteMock = _fixture.Paciente;

        _accountManagerServiceMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(authenticatedUser);
        _accountManagerServiceMock.SetupSequence(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
            .ReturnsAsync((ApplicationUser)null!)
            .ReturnsAsync((ApplicationUser)null!);
        _accountManagerServiceMock.Setup(x => x.UpdateUserAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(true);
        _accountManagerServiceMock.Setup(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>())).ReturnsAsync("NyeVfu3nZTZizATtjZtSbTEU");
        _fixture.MockRabbitMqQueueConfiguration("RabbitMQ:EmailConfirmationQueueName", "ha.queue.mock_queue_name");
        _pacienteRepositoryMock.Setup(x => x.GetByFilterAsync(It.IsAny<Expression<Func<Paciente, bool>>>())).ReturnsAsync(pacienteMock);
        _pacienteRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Paciente>())).ReturnsAsync(1);

        var handler = new UpdateAuthenticatedUserInfoRequestHandler(
            _fixture.HttpContextAccessorMockPaciente.Object,
            _eventBusMock.Object,
            _fixture.ConfigurationMock.Object,
            _accountManagerServiceMock.Object,
            _pacienteRepositoryMock.Object,
            _especialistaRepositoryMock.Object);

        var request = new UpdateAuthenticatedUserInfoRequest(
                NomeCompleto: _fixture.Faker.Person.FullName,
                Telefone: _fixture.Faker.Person.CustomCellPhoneBR(),
                Email: _fixture.Faker.Person.Email,
                Username: _fixture.Faker.Person.UserName);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Exception.Should().BeNull();

        _accountManagerServiceMock.Verify(c => c.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Exactly(2));
        _accountManagerServiceMock.Verify(c => c.UpdateUserAsync(It.IsAny<ApplicationUser>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()), Times.Once);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<EmailConfirmationCreatedEvent>(), "ha.queue.mock_queue_name", ""), Times.Once);
        _pacienteRepositoryMock.Verify(x => x.GetByFilterAsync(It.IsAny<Expression<Func<Paciente, bool>>>(), It.IsAny<Expression<Func<Paciente, object>>[]>()), Times.Once);
        _pacienteRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Paciente>()), Times.Once);
        _especialistaRepositoryMock.Verify(x => x.GetByFilterAsync(It.IsAny<Expression<Func<Especialista, bool>>>(), It.IsAny<Expression<Func<Especialista, object>>[]>()), Times.Never);
        _especialistaRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Especialista>()), Times.Never);
    }

    [Fact(DisplayName = "Deve atualizar os dados do usuário ESPECIALISTA e publicar evento com sucesso")]
    public async Task Handle_ShouldUpdateEspecialistaDataAndPublishEvent_WhenRequestIsValid()
    {
        // Arrange
        var authenticatedUser = _fixture.UserEspecialista;
        var especialistaMock = _fixture.Especialista;

        _accountManagerServiceMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(authenticatedUser);
        _accountManagerServiceMock.SetupSequence(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
            .ReturnsAsync((ApplicationUser)null!)
            .ReturnsAsync((ApplicationUser)null!);
        _accountManagerServiceMock.Setup(x => x.UpdateUserAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(true);
        _accountManagerServiceMock.Setup(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>())).ReturnsAsync("NyeVfu3nZTZizATtjZtSbTEU");
        _fixture.MockRabbitMqQueueConfiguration("RabbitMQ:EmailConfirmationQueueName", "ha.queue.mock_queue_name");
        _especialistaRepositoryMock.Setup(x => x.GetByFilterAsync(It.IsAny<Expression<Func<Especialista, bool>>>())).ReturnsAsync(especialistaMock);
        _especialistaRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Especialista>())).ReturnsAsync(1);

        var handler = new UpdateAuthenticatedUserInfoRequestHandler(
            _fixture.HttpContextAccessorMockEspecialista.Object,
            _eventBusMock.Object,
            _fixture.ConfigurationMock.Object,
            _accountManagerServiceMock.Object,
            _pacienteRepositoryMock.Object,
            _especialistaRepositoryMock.Object);

        var request = new UpdateAuthenticatedUserInfoRequest(
                NomeCompleto: _fixture.Faker.Person.FullName,
                Telefone: _fixture.Faker.Person.CustomCellPhoneBR(),
                Email: _fixture.Faker.Person.Email,
                Username: _fixture.Faker.Person.UserName);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Exception.Should().BeNull();

        _accountManagerServiceMock.Verify(c => c.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Exactly(2));
        _accountManagerServiceMock.Verify(c => c.UpdateUserAsync(It.IsAny<ApplicationUser>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()), Times.Once);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<EmailConfirmationCreatedEvent>(), "ha.queue.mock_queue_name", ""), Times.Once);
        _pacienteRepositoryMock.Verify(x => x.GetByFilterAsync(It.IsAny<Expression<Func<Paciente, bool>>>(), It.IsAny<Expression<Func<Paciente, object>>[]>()), Times.Never);
        _pacienteRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Paciente>()), Times.Never);
        _especialistaRepositoryMock.Verify(x => x.GetByFilterAsync(It.IsAny<Expression<Func<Especialista, bool>>>(), It.IsAny<Expression<Func<Especialista, object>>[]>()), Times.Once);
        _especialistaRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Especialista>()), Times.Once);
    }
}