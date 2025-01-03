namespace RSF.AgendamentoConsultas.Api.Models;

internal sealed class ApiListResponse<T> : ApiResponse<IReadOnlyList<T>>
{
    public ApiListResponse(IReadOnlyList<T> data) 
        : base(data) 
    { }
}