using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Data.Context;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;

namespace RSF.AgendamentoConsultas.Data.Repositories.Common;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected AppDbContext Context { get; }

    public BaseRepository(AppDbContext context) => Context = context;


    public async ValueTask AddAsync(T entity)
        => await Context.Set<T>().AddAsync(entity).ConfigureAwait(false);

    public async ValueTask<IReadOnlyList<T>> GetAllAsync()
        => await Context.Set<T>().ToListAsync().ConfigureAwait(false);

    public async ValueTask<IReadOnlyList<T>> GetByFilterAsync(Expression<Func<T, bool>> filter)
        => await Context.Set<T>().Where(filter).ToListAsync().ConfigureAwait(false);

    public async ValueTask<T> GetByIdAsync(int id)
        => await Context.Set<T>().FindAsync(id).ConfigureAwait(false);

    public async ValueTask RemoveAsync(T entity)
        => await ValueTask.FromResult(Context.Set<T>().Remove(entity)).ConfigureAwait(false);

    public async ValueTask SaveChangesAsync() 
        => await Context.SaveChangesAsync().ConfigureAwait(false);

    public async ValueTask UpdateAsync(T entity)
    {
        Context.Set<T>().Update(entity);
        await Context.SaveChangesAsync().ConfigureAwait(false);
    }
}