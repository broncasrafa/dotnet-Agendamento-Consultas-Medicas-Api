using System.Linq.Expressions;

namespace RSF.AgendamentoConsultas.Domain.Interfaces.Common;

public interface IBaseRepository<T> where T : class
{
    ValueTask<IReadOnlyList<T>> GetAllAsync();
    ValueTask<IReadOnlyList<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object?>>[] includes);
    ValueTask<T> GetByFilterAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object?>>[] includes);
    ValueTask<T> GetByIdAsync(int id);
    ValueTask AddAsync(T entity);
    ValueTask UpdateAsync(T entity);
    ValueTask RemoveAsync(T entity);
    ValueTask<int> SaveChangesAsync();
}