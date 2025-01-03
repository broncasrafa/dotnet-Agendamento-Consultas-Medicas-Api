namespace RSF.AgendamentoConsultas.Api.Models;

internal class ApiResponse<T>
{
    public T Data { get; private set; }
    public ApiResponse(T data) => Data = data;
}