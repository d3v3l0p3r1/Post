using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Base.Repositories.Abstract
{
    public interface IBaseRepository<T, TContext> 
        where T : Entity
        where TContext : DbContext
    {
        T Get(long id);

        Task<T> GetAsync(long id);

        Task UpdateAsync(T entity);

        IQueryable<T> GetAll();

        void Create(T entity);

        Task CreateAsync(T entity);
    }
}
