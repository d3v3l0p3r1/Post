using Microsoft.EntityFrameworkCore;
using Post.Base.Repositories.Abstract;
using Post.Base.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Post.Base.Services.Concrete
{
    public class BaseService<T, TContext> : IBaseService<T, TContext> 
        where T : Entity
        where TContext : DbContext
    {
        protected readonly IBaseRepository<T, TContext> Repository;

        public BaseService(IBaseRepository<T, TContext> repository)
        {
            Repository = repository;
        }

        public virtual Task CreateAsync(T entity)
        {
            return Repository.CreateAsync(entity);
        }

        public virtual Task UpdateAsync(T entity)
        {
            return Repository.UpdateAsync(entity);
        }

    }
}
