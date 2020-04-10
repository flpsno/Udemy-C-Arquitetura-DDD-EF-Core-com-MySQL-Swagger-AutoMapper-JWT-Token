using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MyContext _context;
        private DbSet<T> _dataSet;

        public BaseRepository(MyContext context)
        {
            _context = context;
            _dataSet = _context.Set<T>();
        }
        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> InsertAsync(T entity)
        {
            try
            {
                if (entity.Id == Guid.Empty) 
                {
                    entity.Id = Guid.NewGuid();
                }

                entity.CreatAt = DateTime.UtcNow;
                await _dataSet.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return entity;
        }

        public Task<T> SelectAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> SelectAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                var result = await _dataSet.SingleOrDefaultAsync(p => p.Id.Equals(entity.Id));

                if (result == null)
                    return null;

                entity.UpdateAt = DateTime.UtcNow;
                entity.CreatAt = result.CreatAt;

                _context.Entry(result).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return entity;
        }
    }
}
