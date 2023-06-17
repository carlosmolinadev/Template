using SimpleTemplate.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTemplate.Application.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<T> SelectByIdAsync<Tid>(Tid id);
        Task<IEnumerable<T>> SelectAllAsync();
        Task<IEnumerable<T>> SelectByParameterAsync(QueryParameter queryParameter);
        Task<T> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task SaveAsync<P>(string storedProcedure, P parameters);
        Task<IEnumerable<U>> LoadAsync<U, P>(string storedProcedure, P parameters);
    }
}
