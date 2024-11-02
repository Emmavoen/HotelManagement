using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace HotelManagement.Application.Contracts.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByColumnAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllByColumn(Expression<Func<T, bool>> predicate);
        Task DeleteAsync(int id);
        void UpdateASync(T entity);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetWhereAndIncludeAsync(
        Expression<Func<T, bool>> filter,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
    }
}