using RSF.AgendamentoConsultas.Core.Domain.Models;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.Login;
using RSF.AgendamentoConsultas.Core.Application.Tests.Base;
using RSF.AgendamentoConsultas.Core.Application.Tests.Base.Extensions;
using Moq;
using FluentAssertions;
using Bogus.Extensions.Brazil;


namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Account.Command.Login;

public class LoginRequestHandlerTest : IClassFixture<BaseFixture>
{
    private readonly BaseFixture _fixture;
    private readonly Mock<IAccountManagerService> _accountManagerServiceMock;
    private readonly LoginRequestHandler _handler;

    public LoginRequestHandlerTest(BaseFixture fixture)
    {
        _fixture = fixture;
        _accountManagerServiceMock = new Mock<IAccountManagerService>();
        _handler = new LoginRequestHandler(_accountManagerServiceMock.Object);
    }


    [Fact(DisplayName = "Deve retornar usuário autenticado com sucesso")]
    public async Task Handle_ShouldReturnAuthenticatedUser_WhenLoginIsSuccessful()
    {
        // Arrange
        var request = new LoginRequest(Email: _fixture.Faker.Person.Email, Password: "Usuario@123");
        var authenticatedUser = new UsuarioAutenticadoModel
        {
            Id = _fixture.UserId,
            Nome = _fixture.Faker.Person.FullName,
            Documento = _fixture.Faker.Person.Cpf(),
            Username = _fixture.Faker.Person.UserName,
            Email = _fixture.Faker.Person.Email,
            Telefone = _fixture.Faker.Person.CustomCellPhoneBR(),
            IsActive = true,
            CreatedAt = DateTime.Now.AddDays(-7),
            Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9"
        };
        _accountManagerServiceMock.Setup(x => x.LoginAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(authenticatedUser);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.Credentials.Token.Should().Be(authenticatedUser.Token);
        result.Value.Usuario.Id.Should().Be(authenticatedUser.Id);

        _accountManagerServiceMock.Verify(x => x.LoginAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }


    [Fact(DisplayName = "Deve retornar erro ao falhar na autenticação em um cenário com token vazio")]
    public async Task Handle_ShouldReturnError_WhenTokenIsEmpty()
    {
        // Arrange
        var request = new LoginRequest(Email: _fixture.Faker.Person.Email, Password: "Usuario@123");        
        var authenticatedUser = new UsuarioAutenticadoModel
        {
            Id = _fixture.UserId,
            Nome = _fixture.Faker.Person.FullName,
            Documento = _fixture.Faker.Person.Cpf(),
            Username = _fixture.Faker.Person.UserName,
            Email = _fixture.Faker.Person.Email,
            Telefone = _fixture.Faker.Person.CustomCellPhoneBR(),
            IsActive = true,
            CreatedAt = DateTime.Now.AddDays(-7),
            Token = string.Empty // Simula token inválido
        };
        _accountManagerServiceMock.Setup(x => x.LoginAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(authenticatedUser);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Value.Should().BeNull();
        result.Exception.Should().BeOfType<OperationErrorException>()
            .Which.Message.Should().Be("Falha ao realizar o login do usuário.");

        _accountManagerServiceMock.Verify(x => x.LoginAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
}