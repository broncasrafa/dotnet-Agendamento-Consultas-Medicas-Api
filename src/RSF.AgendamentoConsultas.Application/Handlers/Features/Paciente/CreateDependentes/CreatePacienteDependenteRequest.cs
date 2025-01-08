using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.CreateDependentes;

public class CreatePacienteDependenteRequest : IRequest<Result<PacienteDependenteResponse>>
{
    public int PacientePrincipalId { get; set; }
    public string NomeCompleto { get; set; }
    public string Email { get; set; }
    public string Genero { get; set; }
    public DateTime DataNascimento { get; set; }
    public string CPF { get; set; }
    public string Telefone { get; set; }
}