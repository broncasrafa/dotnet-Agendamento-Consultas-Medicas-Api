namespace RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;

public class InputRequestDataInvalidException : BaseException
{
    public IEnumerable<string> Errors { get; }

    public InputRequestDataInvalidException(IDictionary<string, IEnumerable<string>> errors)
        : base("Requisição com dados de entrada inválidos", System.Net.HttpStatusCode.BadRequest)
    {
        //Errors = errors.Select(c => $"{c.Key}: {string.Join(", ", c.Value)}");
        Errors = errors.Select<KeyValuePair<string, IEnumerable<string>>, string>((KeyValuePair<string, IEnumerable<string>> c) => $"{c.Key}: {string.Join(", ", c.Value)}");
    }

    public InputRequestDataInvalidException(string key, string errorMessage)
        : base("Requisição com dados de entrada inválidos", System.Net.HttpStatusCode.BadRequest)
    {
        IDictionary<string, IEnumerable<string>> errors = new Dictionary<string, IEnumerable<string>>
        {
            { key, new List<string> { errorMessage } }
        };
        Errors = errors.Select<KeyValuePair<string, IEnumerable<string>>, string>((KeyValuePair<string, IEnumerable<string>> c) => $"{c.Key}: {string.Join(", ", c.Value)}");
    }
}