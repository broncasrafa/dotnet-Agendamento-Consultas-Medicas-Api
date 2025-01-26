using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.AddLocalAtendimento;

public record AddLocalAtendimentoRequest(
    int EspecialistaId,
    string Nome,
    string Logradouro,
    string Complemento,
    string Bairro,
    string Cep,
    string Cidade,
    string Estado,
    decimal? Preco,
    string TipoAtendimento,
    string Telefone,
    string Whatsapp
) : IRequest<Result<bool>>;