using Libreria.Datos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Negocio.Base
{
    public interface IRepository<T,K>
    {

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T,bool>> predicate);

        Task<T> GetByIdAsync(K id);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteteAsync(T entity);

        Task<bool> ExistsById(K id);
    }
}
