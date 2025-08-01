﻿using System.Linq.Expressions;

namespace RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;

public interface IBaseRepository<T> where T : class
{
    ValueTask<IReadOnlyList<T>> GetAllAsync();
    ValueTask<IReadOnlyList<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
    ValueTask<T> GetByFilterAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
    ValueTask<T> GetByIdAsync(int id);
    ValueTask<bool> ExistsByIdAsync(int id);
    /// <summary>
    /// Adiciona os dados da entidade
    /// </summary>
    /// <param name="entity">entidade</param>
    /// <returns>a quantidade de linhas afetadas na transação (rowsAffected)</returns>
    ValueTask<int> AddAsync(T entity);
    /// <summary>
    /// Atualiza os dados da entidade
    /// </summary>
    /// <param name="entity">entidade</param>
    /// <returns>a quantidade de linhas afetadas na transação (rowsAffected)</returns>
    ValueTask<int> UpdateAsync(T entity);
    /// <summary>
    /// Atualiza os dados da coleção de entidades
    /// </summary>
    /// <param name="collection">coleção de entidades</param>
    /// <returns>a quantidade de linhas afetadas na transação (rowsAffected)</returns>
    ValueTask<int> UpdateRangeAsync(IEnumerable<T> collection);
    /// <summary>
    /// Remove os dados da entidade
    /// </summary>
    /// <param name="entity">entidade</param>
    /// <returns>a quantidade de linhas afetadas na transação (rowsAffected)</returns>
    ValueTask<int> RemoveAsync(T entity);
    /// <summary>
    /// Atualiza o status da entidade de ativo para inativo e vice versa
    /// </summary>
    /// <param name="entity">entidade</param>
    /// <returns>a quantidade de linhas afetadas na transação (rowsAffected)</returns>
    ValueTask<int> ChangeStatusAsync(T entity);
    ValueTask<int> SaveChangesAsync();


    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<string>? includes = null);
    Task<T> GetAsync(Expression<Func<T, bool>> expression, List<string>? includes = null);
}