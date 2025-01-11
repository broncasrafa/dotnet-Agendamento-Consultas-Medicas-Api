using System.Linq.Expressions;

namespace RSF.AgendamentoConsultas.Domain.Interfaces.Common;

public interface IBaseRepository<T> where T : class
{
    ValueTask<IReadOnlyList<T>> GetAllAsync();
    ValueTask<IReadOnlyList<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object?>>[] includes);
    ValueTask<T> GetByFilterAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object?>>[] includes);
    ValueTask<T> GetByIdAsync(int id);
    ValueTask AddAsync(T entity);
    /// <summary>
    /// Atualiza os dados da entidade
    /// </summary>
    /// <param name="entity">entidade</param>
    /// <returns>a quantidade de linhas afetadas na transação (rowsAffected</returns>
    ValueTask<int> UpdateAsync(T entity);
    ValueTask RemoveAsync(T entity);
    /// <summary>
    /// Atualiza o status da entidade de ativo para inativo e vice versa
    /// </summary>
    /// <param name="entity">entidade</param>
    /// <returns>a quantidade de linhas afetadas na transação (rowsAffected</returns>
    ValueTask<int> ChangeStatusAsync(T entity);
    ValueTask<int> SaveChangesAsync();
}