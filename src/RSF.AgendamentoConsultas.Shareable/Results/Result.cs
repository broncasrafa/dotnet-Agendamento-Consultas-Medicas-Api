using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RSF.AgendamentoConsultas.Shareable.Results;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }


}