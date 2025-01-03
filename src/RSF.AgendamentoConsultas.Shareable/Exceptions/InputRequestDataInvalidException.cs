namespace RSF.AgendamentoConsultas.Shareable.Exceptions;

public class InputRequestDataInvalidException : BaseException
{
    public IEnumerable<string> Errors { get; }

    public InputRequestDataInvalidException(IDictionary<string, IEnumerable<string>> errors)
        : base("Input request data is invalid", System.Net.HttpStatusCode.BadRequest) => Errors = errors.Select(c => $"{c.Key}: {string.Join(", ", c.Value)}");
}