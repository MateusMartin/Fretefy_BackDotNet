using System.Threading.Tasks;
using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Services;
using Fretefy.Test.Infra.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fretefy.Test.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Apenas devido ao uso do InMemory
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TestDbContext>();
                context.Database.EnsureCreated();

                var service = scope.ServiceProvider.GetRequiredService<ICidadeService>();
                await service.PopularCidadesAsync();
            }

            host.Run();


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
