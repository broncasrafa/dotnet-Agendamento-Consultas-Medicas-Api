using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Command.CreatePaciente;

public class CreatePacienteRequest : IRequest<Result<PacienteResponse>>
{
    public string NomeCompleto { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Telefone { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Genero { get; set; }
    public string CPF { get; set; }
    public bool TermoUsoAceito { get; set; }
    public bool AutorizarRecebimentoInformacoes { get; set; }
}