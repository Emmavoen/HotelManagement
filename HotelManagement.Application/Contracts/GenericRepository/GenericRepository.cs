using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HotelManagement.Application.Contracts.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByColumnAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task DeleteAsync(int id);
        void UpdateASync(T entity);
        Task<T> AddAsync(T entity);
    }
}