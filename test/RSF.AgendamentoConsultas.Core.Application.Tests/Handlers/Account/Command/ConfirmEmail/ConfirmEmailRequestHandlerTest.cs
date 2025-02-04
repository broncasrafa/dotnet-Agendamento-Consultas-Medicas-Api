using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ConfirmEmail;
using RSF.AgendamentoConsultas.Core.Application.Tests.Base;
using RSF.AgendamentoConsultas.Core.Application.Tests.Base.Extensions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using FluentAssertions;
using Moq;
using Bogus.Extensions.Brazil;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Account.Command.ConfirmEmail;

public class ConfirmEmailRequestHandlerTest : IClassFixture<BaseFixture>
{
    private readonly BaseFixture _fixture;
    private readonly ConfirmEmailRequestHandler _handler;
    private readonly Mock<IAccountManagerService> _accountManagerServiceMock;
    private readonly Mock<IPacienteRepository> _pacienteRepositoryMock;

    public ConfirmEmailRequestHandlerTest(BaseFixture fixture)
    {
        _fixture = fixture;
        _accountManagerServiceMock = new Mock<IAccountManagerService>();
        _pacienteRepositoryMock = new Mock<IPacienteRepository>();
        _handler = new ConfirmEmailRequestHandler(_accountManagerServiceMock.Object, _pacienteRepositoryMock.Object);
    }

    [Fact(DisplayName = "Deve confirmar o e-mail e atualizar o paciente com sucesso")]
    public async Task Handle_ShouldConfirmEmailAndUpdatePaciente_WhenPacienteExists()
    {
        // Arrange
        var request = new ConfirmEmailRequest(UserId: "7009b7bc-c253-4998-b72a-75408186edf6", Code: "fjDOQyXsDy5SrqwK3QCXEi4HGbJyNt6s");
        var paciente = new Paciente(
            userId: request.UserId,
            nome: _fixture.Faker.Person.FullName,  
            cpf: _fixture.Faker.Person.Cpf(), 
            email: _fixture.Faker.Person.Email, 
            telefone: _fixture.Faker.Person.CustomCellPhoneBR(), 
            genero: _fixture.Faker.CustomGender(), 
            dataNascimento: _fixture.Faker.CustomDateOfBirth()) { EmailVerificado = false };

        _accountManagerServiceMock.Setup(c => c.ConfirmEmailAsync(request.UserId, request.Code)).ReturnsAsync(true);
        _pacienteRepositoryMock.Setup(c => c.GetByFilterAsync(c => c.UserId == request.UserId)).ReturnsAsync(paciente);
        _pacienteRepositoryMock.Setup(c => c.UpdateAsync(paciente)).Returns(new ValueTask<int>(1));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Value.Should().BeTrue();
        paciente.EmailVerificado.Should().BeTrue();

        _pacienteRepositoryMock.Verify(x => x.UpdateAsync(It.Is<Paciente>(p => p.EmailVerificado!.Value)), Times.Once);
    }

    [Fact(DisplayName = "Deve retornar erro ao falhar na confirmação do e-mail")]
    public async Task Handle_ShouldReturnError_WhenEmailConfirmationFails()
    {
        // Arrange
        var request = new ConfirmEmailRequest(UserId: "7009b7bc-c253-4998-b72a-75408186edf6", Code: "fjDOQyXsDy5SrqwK3QCXEi4HGbJyNt6s");

        _accountManagerServiceMock.Setup(x => x.ConfirmEmailAsync(request.UserId, request.Code)).ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Value.Should().BeFalse();
        result.Exception.Should().BeOfType<OperationErrorException>()
            .Which.Message.Should().Be("Falha ao confirmar o e-mail do usuário.");

        _pacienteRepositoryMock.Verify(x => x.UpdateAsync(It.Is<Paciente>(p => p.EmailVerificado!.Value)), Times.Never);
    }
}
