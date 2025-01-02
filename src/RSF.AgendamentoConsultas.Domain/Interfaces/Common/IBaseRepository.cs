using System.Linq.Expressions;

namespace RSF.AgendamentoConsultas.Domain.Interfaces.Common;

public interface IBaseRepository<T> where T : class
{
    ValueTask<IReadOnlyList<T>> GetAllAsync();
    ValueTask<IReadOnlyList<T>> GetByFilterAsync(Expression<Func<T, bool>> filter);
    ValueTask<T> GetByIdAsync(Guid id);
    ValueTask AddAsync(T entity);
    ValueTask UpdateAsync(T entity);
    ValueTask RemoveAsync(T entity);
    ValueTask SaveChangesAsync();
}