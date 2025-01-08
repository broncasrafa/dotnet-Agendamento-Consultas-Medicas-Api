using System.Linq.Expressions;
using RSF.AgendamentoConsultas.Shareable.Results;

namespace RSF.AgendamentoConsultas.Domain.Interfaces.Common;

public interface IBaseRepository<T> where T : class
{
    ValueTask<IReadOnlyList<T>> GetAllAsync();
    ValueTask<IReadOnlyList<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filter);
    ValueTask<T> GetByFilterAsync(Expression<Func<T, bool>> filter);
    ValueTask<T> GetByIdAsync(int id);
    ValueTask AddAsync(T entity);
    ValueTask UpdateAsync(T entity);
    ValueTask RemoveAsync(T entity);
    ValueTask SaveChangesAsync();
}