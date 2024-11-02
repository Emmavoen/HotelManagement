using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.GenericRepository;
using HotelManagement.Persistence.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace HotelManagement.Persistence.RepositoryImplementation.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        protected DbSet<T> _dbSet;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            var result = await _dbSet.AddAsync(entity);
            return result.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _dbSet.FindAsync(id);
            _dbSet.Remove(existing);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var result = await _dbSet.ToListAsync();
            // await appDbContext.SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<T>> GetAllByColumn(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T> GetByColumnAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public void UpdateASync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

        }
        public async Task<IEnumerable<T>> GetWhereAndIncludeAsync(
        Expression<Func<T, bool>> filter,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (include != null)
            {
                query = include(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }
    }
}