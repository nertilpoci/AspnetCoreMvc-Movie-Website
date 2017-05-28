using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Movies.DAL
{
   public interface IRepository<TEntity> where TEntity:class
    {

        #region Async
        Task<TEntity> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync(int count=0);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, int count=0);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync();
        #endregion
        #region NotAsync
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll(int count=0);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate,int count=0);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        bool Any(Expression<Func<TEntity, bool>> predicate);
        void Update(TEntity entity);
        bool Any();
        #endregion

    }
}
