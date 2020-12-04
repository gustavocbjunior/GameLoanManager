using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameLoanManager.Data.Repository
{
    public class RepositoryBase<T> : IRepository<T>, IDisposable where T : Entity
    {
        protected readonly Context _context;
        private DbSet<T> _dbset;

        public RepositoryBase(Context context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public async Task<T> DeleteAsync(long id)
        {
            var result = await _dbset.SingleOrDefaultAsync(x => x.Id.Equals(id));
            if (result == null)
            {
                return null;
            }

            _dbset.Remove(result);
            await _context.SaveChangesAsync();
            
            return result;
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _dbset.AnyAsync(p => p.Id.Equals(id));
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _dbset.FindAsync(id);
        }

        public async Task<T> InsertAsync(T entity)
        {
            entity.CreateAt = DateTime.UtcNow;
            _dbset.Add(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var result = await _dbset.SingleOrDefaultAsync(p => p.Id.Equals(entity.Id));
            if (result == null)
            {
                return null;
            }

            entity.CreateAt = result.CreateAt;

            _context.Entry(result).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async void Dispose()
        {
            if (_context != null)
            {
                await _context.DisposeAsync();
                GC.SuppressFinalize(this);
            }
        }
    }
}