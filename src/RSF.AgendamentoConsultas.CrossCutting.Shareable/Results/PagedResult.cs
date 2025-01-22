namespace RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;

public class PagedResult<T>
{
    /// <summary>
    /// lista dos objetos retornados na pagina
    /// </summary>
    public IEnumerable<T> Results { get; set; }

    /// <summary>
    /// total de registros da tabela
    /// </summary>
    public int Total { get; set; }

    /// <summary>
    /// informa se existe uma proxima pagina
    /// </summary>
    public bool HasNextPage { get; }

    /// <summary>
    /// informa o numero da proxima pagina
    /// </summary>
    public int? NextPage { get; }

    /// <summary>
    /// informa o numero da pagina final, com base no total de registros
    /// </summary>
    public int EndPage { get; }

    //public PagedResult()
    //{            
    //}

    public PagedResult(IEnumerable<T> data, int totalCount, int pageNumber, int pageSize)
    {
        Total = totalCount;
        Results = data;

        // Calcula as propriedades adicionais
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        var hasNextPage = pageNumber < totalPages;
        var nextPage = hasNextPage ? pageNumber + 1 : (int?)null;

        HasNextPage = hasNextPage;
        NextPage = nextPage;
        EndPage = totalPages;
    }
}
