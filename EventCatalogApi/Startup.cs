using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCatalogApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EventCatalogApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

       public void ConfigureServices(IServiceCollection services)
        {
            var server = Configuration["DatabaseServer"];
            var database = Configuration["DatabaseName"];
            var user = Configuration["DatabaseUser"];
            var password = Configuration["DatabaseUserPassword"];
            var connectionString = $"Server={server};Database={database};User ID={user};Password={password};MultipleActiveResultSets=true";

            services.AddMvc();
            services.AddDbContext<CatalogContext>(
                options =>
                    options.UseSqlServer(connectionString)
                );
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1",
                    new Swashbuckle.AspNetCore.Swagger.Info
                    {
                        Title = "Event catalog HTTP API",
                        Version = "V1",
                        Description = "Display catalog of Events",
                        TermsOfService = "Terms of service"
                    });
            });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwaggerUI()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/swagger/v1/swagger.json",
                        "Eventcatalogapi v1");
                });

            app.UseMvc();
        }
    }
}
