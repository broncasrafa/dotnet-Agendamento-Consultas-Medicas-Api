using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;


namespace RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object?>>[] includes) where T : class
    {
        foreach (var include in includes)
            query = query.Include(include);

        string sqlQuery = query.ToQueryString();
        return query;
    }
}