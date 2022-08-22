using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Optmni.BL.Manager.Interface;

namespace Optmni.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

                var backendAdminManager = scope.ServiceProvider.GetRequiredService<IBackendAdminManager>();
                backendAdminManager.MigrateDatabases().GetAwaiter().GetResult();
                backendAdminManager.CreateTempAccount().GetAwaiter().GetResult();

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
