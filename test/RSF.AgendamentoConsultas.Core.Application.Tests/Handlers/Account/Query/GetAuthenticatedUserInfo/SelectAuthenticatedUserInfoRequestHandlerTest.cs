using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.Core.Application.Tests.Base;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Query.GetAuthenticatedUserInfo;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using FluentAssertions;
using Moq;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Account.Query.GetAuthenticatedUserInfo;

public class SelectAuthenticatedUserInfoRequestHandlerTest : IClassFixture<BaseFixture>
{
    private readonly SelectAuthenticatedUserInfoRequestHandler _handlerPaciente;
    private readonly SelectAuthenticatedUserInfoRequestHandler _handlerEspecialista;
    private readonly SelectAuthenticatedUserInfoRequestHandler _handlerAdmin;
    private readonly Mock<IAccountManagerService> _accountManagerServiceMock;
    private readonly BaseFixture _fixture;

    public SelectAuthenticatedUserInfoRequestHandlerTest(BaseFixture fixture)
    {
        _fixture = fixture;
        _accountManagerServiceMock = new Mock<IAccountManagerService>();

        _handlerPaciente = new SelectAuthenticatedUserInfoRequestHandler(_accountManagerServiceMock.Object, _fixture.HttpContextAccessorMockPaciente.Object);
        _handlerEspecialista = new SelectAuthenticatedUserInfoRequestHandler(_accountManagerServiceMock.Object, _fixture.HttpContextAccessorMockEspecialista.Object);
        _handlerAdmin = new SelectAuthenticatedUserInfoRequestHandler(_accountManagerServiceMock.Object, _fixture.HttpContextAccessorMockAdmin.Object);
    }

    [Fact(DisplayName = "Deve retornar usuário do perfil Paciente autenticado com sucesso")]
    public async Task Handle_ShouldReturnAuthenticatedUser_WhenUserIsAuthenticated_AsPaciente()
    {
        // Arrange
        var userPrincipal = _fixture.HttpContextAccessorMockPaciente.Object.HttpContext.User;
        var expectedUser = _fixture.UserPaciente;

        _accountManagerServiceMock.Setup(x => x.GetUserAsync(userPrincipal)).ReturnsAsync(expectedUser);

        var request = new SelectAuthenticatedUserInfoRequest();

        // Act
        var result = await _handlerPaciente.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value!.Usuario.Nome.Should().Be(expectedUser.NomeCompleto);

        // Assert: Verifica se o usuário possui o perfil Admin
        var roleClaim = userPrincipal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);
        roleClaim.Should().NotBeNull();
        roleClaim!.Value.Should().Be(ETipoPerfilAcesso.Paciente.ToString());
    }

    [Fact(DisplayName = "Deve retornar usuário do perfil Especialista autenticado com sucesso")]
    public async Task Handle_ShouldReturnAuthenticatedUser_WhenUserIsAuthenticated_AsEspecialista()
    {
        // Arrange
        var userPrincipal = _fixture.HttpContextAccessorMockEspecialista.Object.HttpContext.User;
        var expectedUser = _fixture.UserEspecialista;

        _accountManagerServiceMock.Setup(x => x.GetUserAsync(userPrincipal)).ReturnsAsync(expectedUser);

        var request = new SelectAuthenticatedUserInfoRequest();

        // Act
        var result = await _handlerEspecialista.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value!.Usuario.Nome.Should().Be(expectedUser.NomeCompleto);

        // Assert: Verifica se o usuário possui o perfil Admin
        var roleClaim = userPrincipal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);
        roleClaim.Should().NotBeNull();
        roleClaim!.Value.Should().Be(ETipoPerfilAcesso.Profissional.ToString());
    }

    [Fact(DisplayName = "Deve retornar usuário do perfil Administrador autenticado com sucesso")]
    public async Task Handle_ShouldReturnAuthenticatedUser_WhenUserIsAuthenticated_AsAdministrador()
    {
        // Arrange
        var userPrincipal = _fixture.HttpContextAccessorMockAdmin.Object.HttpContext.User;
        var expectedUser = _fixture.UserAdmin;

        _accountManagerServiceMock.Setup(x => x.GetUserAsync(userPrincipal)).ReturnsAsync(expectedUser);

        var request = new SelectAuthenticatedUserInfoRequest();

        // Act
        var result = await _handlerAdmin.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value!.Usuario.Nome.Should().Be(expectedUser.NomeCompleto);

        // Assert: Verifica se o usuário possui o perfil Admin
        var roleClaim = userPrincipal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);
        roleClaim.Should().NotBeNull();
        roleClaim!.Value.Should().Be(ETipoPerfilAcesso.Administrador.ToString());
    }


    [Fact(DisplayName = "Deve lançar UnauthorizedRequestException quando usuário não está autenticado")]
    public async Task Handle_ShouldThrowUnauthorizedRequestException_WhenUserIsNotAuthenticated()
    {
        // Arrange
        Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        SelectAuthenticatedUserInfoRequestHandler handler = new SelectAuthenticatedUserInfoRequestHandler(
            _accountManagerServiceMock.Object, 
            httpContextAccessorMock.Object);
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()); // Sem User no contexto

        var request = new SelectAuthenticatedUserInfoRequest();

        // Act
        var act = async () => await handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedRequestException>()
            .WithMessage("Usuário não está autenticado na plataforma");
    }


    [Fact(DisplayName = "Deve lançar NotFoundException quando usuário não está autenticado")]
    public async Task Handle_ShouldThrowNotFoundException_WhenUserIsNotAuthenticated()
    {
        // Arrange
        Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()); // Sem User no contexto

        var request = new SelectAuthenticatedUserInfoRequest();

        // Act
        var act = async () => await _handlerPaciente.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Usuário não está autenticado na plataforma");
    }
}




