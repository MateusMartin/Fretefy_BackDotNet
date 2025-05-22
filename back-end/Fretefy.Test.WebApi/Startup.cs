using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Domain.Services;
using Fretefy.Test.Infra.EntityFramework;
using Fretefy.Test.Infra.EntityFramework.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fretefy.Test.WebApi
{
    public class Startup
    {

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IbgeSettings>(_configuration.GetSection("Ibge"));
            services.AddScoped<DbContext, TestDbContext>();
            services.AddDbContext<TestDbContext>((provider, options) =>
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnectionSqlite");
                options.UseSqlite(connectionString);
            });

            services.AddCors(options =>
             {
                 options.AddPolicy("AllowAngularApp",
                     builder =>
                     {
                         var angularClientUrl = _configuration["Cors:AngularClient"];
                         builder.WithOrigins(angularClientUrl) // URL do seu frontend Angular
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                     });
             });



            ConfigureInfraService(services);
            ConfigureDomainService(services);

            services.AddMvc()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);





        }

        private void ConfigureDomainService(IServiceCollection services)
        {

            services.AddScoped<ICidadeService, CidadeService>();
            services.AddScoped<IRegiaoService, RegiaoService>();
            services.AddScoped<IRegiaoCidadeService, RegiaoCidadeService>();
        }

        private void ConfigureInfraService(IServiceCollection services)
        {
            services.AddScoped<ICidadeRepository, CidadeRepository>();
            services.AddScoped<IRegiaoRepository, RegiaoRepository>();
            services.AddScoped<IRegiaoCidadeRepository, RegiaoCidadeRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowAngularApp");
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
