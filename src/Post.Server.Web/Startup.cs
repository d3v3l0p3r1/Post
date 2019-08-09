using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Post.Base.Repositories.Abstract;
using Post.Base.Repositories.Concrete;
using Post.Server.Contexts;
using Post.Server.Entities;
using Post.Server.Services.Abstract;
using Post.Server.Services.Concrete;

namespace Post.Server.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddEntityFrameworkNpgsql().AddDbContext<ServerContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString(nameof(ServerContext));
                options.UseNpgsql(connectionString, c => c.MigrationsAssembly("Post.Server"));
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info()
                {
                    Title = "Server",
                    Version = "v1"
                });
            });

            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IBaseRepository<ServerMessage, ServerContext>, BaseRepository<ServerMessage, ServerContext>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Server API V1");
                config.RoutePrefix = string.Empty;
            });

            InitializeDatabase(app);
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ServerContext>();
                context.Database.Migrate();
            }
        }
    }
}
