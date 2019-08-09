using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Post.Base.Services.Abstract
{
    public interface IBaseService<T, TContext> 
        where T : Entity
        where TContext : DbContext
    {
        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

    }
}
