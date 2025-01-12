using MediatR;

namespace RSF.AgendamentoConsultas.Domain.MessageBus.Events;

/// <summary>
/// A ideia da classe Message é que alguma requisição estamos esperando um retorno booleano, verdadeiro ou falso, 
/// no comando de requisição foi enviado ou a mensagem foi processada e assim por diante?
/// </summary>
public abstract class Message : IRequest<bool>
{
    public string MessageType { get; protected set; }

    protected Message()
    {
        MessageType = GetType().Name;
    }
}