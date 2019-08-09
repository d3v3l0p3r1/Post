using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.PostgreSql;
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
using Post.Client.Contexts;
using Post.Client.Entities;
using Post.Client.Services.Abstract;
using Post.Client.Services.Concrete;

namespace Post.Client.Web
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

            services.AddEntityFrameworkNpgsql().AddDbContext<ClientContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString(nameof(ClientContext));

                options.UseNpgsql(connectionString, c => c.MigrationsAssembly("Post.Client"));
            });

            services.AddHangfire(config =>
            {
                config.UsePostgreSqlStorage(Configuration.GetConnectionString(nameof(ClientContext)));
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info()
                {
                    Title = "Client",
                    Version = "v1"
                });
            });

            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IBaseRepository<ClientMessage, ClientContext>, BaseRepository<ClientMessage, ClientContext>>();
            services.AddSingleton<IHttpClientWrapper, HttpClientWrapper>();

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
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Client API V1");
                config.RoutePrefix = string.Empty;
            });

            app.UseHangfireServer();
            app.UseHangfireDashboard();

            InitializeDatabase(app);
                        
            RecurringJob.AddOrUpdate<IMessageService>(x => x.ProccessInvalidMessages(), Cron.Minutely);
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ClientContext>();
                context.Database.Migrate();
            }
        }
    }
}
