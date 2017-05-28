using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Movies.DAL
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        private DbSet<TEntity> _entities;
        public Repository(DbContext context)
        {
            Context = context;
            _entities = context.Set<TEntity>();
        }
        #region Async


        public async Task<TEntity> GetAsync(int id)
        {

           return await _entities.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(int count=0)
        {
           
            return  count==0? await _entities.ToListAsync(): await _entities.Take(count).ToListAsync();
        }

      

        public async Task AddAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
             await _entities.AddRangeAsync(entities);
        }

       

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.SingleOrDefaultAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.AnyAsync(predicate);
        }
        public async Task<bool> AnyAsync()
        {
            return await _entities.AnyAsync();
        }

        public  async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate,int count=0)
        {
            return count==0? await _entities.Where(predicate).ToListAsync() : await _entities.Where(predicate).Take(count).ToListAsync();
        }
     


        #endregion
        #region NotAsync
        public TEntity Get(int id)
        {
            return  _entities.Find(id);
        }

        public IEnumerable<TEntity> GetAll(int count=0)
        {
            return count==0?  _entities.ToList() : _entities.Take(count).ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate,int count=0)
        {
            return count == 0 ? _entities.Where(predicate).ToList(): _entities.Where(predicate).Take(count).ToList();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
           
            return  _entities.SingleOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
             _entities.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
             _entities.AddRange(entities);
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Any(predicate);
        }

        public bool Any()
        {
            return  _entities.Any();
        }
        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }

       
        public void Update(TEntity entity)
        {

            Context.Entry(entity).State = EntityState.Modified;
           
           
        }
        #endregion

    }
}
