using Microsoft.EntityFrameworkCore;
using Post.Client.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Post.Client.Contexts
{
    public class ClientContext : DbContext 
    {
        public DbSet<ClientMessage> Messages { get; set; }

        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        {

        }
    }
}
