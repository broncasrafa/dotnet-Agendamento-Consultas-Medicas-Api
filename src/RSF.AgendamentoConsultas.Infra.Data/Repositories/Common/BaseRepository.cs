using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Infra.Data.Context;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;

namespace RSF.AgendamentoConsultas.Infra.Data.Repositories.Common;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected AppDbContext Context { get; }

    public BaseRepository(AppDbContext context) => Context = context;

    
    public async ValueTask<IReadOnlyList<T>> GetAllAsync()
        => await Context.Set<T>().ToListAsync().ConfigureAwait(false);

    public async ValueTask<IReadOnlyList<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object?>>[] includes)
        => (includes is null)
            ? await Context.Set<T>().Where(filter).ToListAsync().ConfigureAwait(false)
            : await Context.Set<T>().IncludeMultiple(includes).Where(filter).ToListAsync().ConfigureAwait(false);

    public async ValueTask<T> GetByFilterAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object?>>[] includes)
        => (includes is null)
            ? await Context.Set<T>().SingleOrDefaultAsync(filter).ConfigureAwait(false)
            : await Context.Set<T>().IncludeMultiple(includes).SingleOrDefaultAsync(filter).ConfigureAwait(false);

    public async ValueTask<T> GetByIdAsync(int id)
        => await Context.Set<T>().FindAsync(id).ConfigureAwait(false);

    public async ValueTask<bool> ExistsByIdAsync(int id)
        => await Context.Set<T>().FindAsync(id) != null;



    public async ValueTask<int> AddAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity).ConfigureAwait(false);
        return await Context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async ValueTask<int> UpdateAsync(T entity)
    {
        Context.Set<T>().Update(entity);
        return await Context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async ValueTask<int> UpdateRangeAsync(IEnumerable<T> collection)
    {
        if (collection == null || !collection.Any())
            throw new ArgumentException("A coleção não pode ser nula ou vazia.", nameof(collection));

        Context.Set<T>().UpdateRange(collection);
        
        return await Context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async ValueTask<int> RemoveAsync(T entity)
    {
        await ValueTask.FromResult(Context.Set<T>().Remove(entity)).ConfigureAwait(false);
        return await Context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async ValueTask<int> ChangeStatusAsync(T entity)
        => await UpdateAsync(entity).ConfigureAwait(false);

    public async ValueTask<int> SaveChangesAsync() 
        => await Context.SaveChangesAsync().ConfigureAwait(false);

    public async ValueTask<PagedResult<TResult>> BindQueryPagedAsync<TResult, TKey>(IQueryable<TResult> query, int pageNumber, int pageSize, Expression<Func<TResult, TKey>> orderBy)
    {
        var orderedQuery = query.OrderBy(orderBy);

        var totalCount = await orderedQuery.CountAsync();

        var paginatedData = await orderedQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        //var sql = query.ToQueryString();
        //Console.WriteLine(sql);
        return new PagedResult<TResult>(data: paginatedData, totalCount: totalCount, pageSize, pageNumber);
    }


    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<string>? includes = null)
    {
        IQueryable<T> query = Context.Set<T>();
        if (expression != null)
            query = query.Where(expression);

        if (includes != null && includes.Any())
        {
            foreach (var include in includes)
            {
                query = EntityFrameworkQueryableExtensions.Include(query, include);
            }
        }
        if (orderBy != null)
            query = orderBy(query);

        return await EntityFrameworkQueryableExtensions.ToListAsync(query).ConfigureAwait(false);
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> expression, List<string>? includes = null)
    {
        IQueryable<T> query = Context.Set<T>();

        if (includes != null && includes.Any())
        {
            foreach (var include in includes)
            {
                query = EntityFrameworkQueryableExtensions.Include(query, include);
            }
        }

        return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(query, expression).ConfigureAwait(false);
    }
}