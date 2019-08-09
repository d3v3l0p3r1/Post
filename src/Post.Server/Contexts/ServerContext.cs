using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Post.Server.Entities;

namespace Post.Server.Contexts
{
    public class ServerContext : DbContext
    {
        public DbSet<ServerMessage> Messages { get; set; }

        public ServerContext(DbContextOptions<ServerContext> options) : base(options)
        {

        }
    }
}
