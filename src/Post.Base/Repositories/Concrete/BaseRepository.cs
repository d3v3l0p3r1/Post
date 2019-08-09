using Microsoft.EntityFrameworkCore;
using Post.Base.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Base.Repositories.Concrete
{
    public class BaseRepository<T, TContext> : IBaseRepository<T, TContext> 
        where T : Entity
        where TContext : DbContext
    {
        protected readonly TContext context;
        protected readonly DbSet<T> dbSet;

        public BaseRepository(TContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public virtual void Create(T entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public virtual Task CreateAsync(T entity)
        {
            dbSet.Add(entity);
            return context.SaveChangesAsync();
        }

        public virtual T Get(long id)
        {
            return dbSet.Single(x => x.ID == id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public virtual Task<T> GetAsync(long id)
        {
            return dbSet.SingleAsync(x => x.ID == id);
        }

        public Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            return context.SaveChangesAsync();
        }
    }
}
