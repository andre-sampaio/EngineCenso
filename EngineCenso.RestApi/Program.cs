using System;
using System.Threading.Tasks;
using EngineCenso.DataAccess;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;
using Microsoft.Extensions.Logging;

namespace EngineCenso.RestApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                var host = BuildWebHost(args);

                using (var scope = host.Services.CreateScope())
                {
                    var serviceProvider = scope.ServiceProvider;

                    Task.Run(async () =>
                    {
                        EngineCensoContextInitializer initializer = new EngineCensoContextInitializer(serviceProvider.GetService<IEngineCensoContext>(), serviceProvider.GetService<IHashingAlgorithm>());
                        await initializer.Seed(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == EnvironmentName.Development);
                    }).Wait();
                }

                host.Run();
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Exception not caught");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                })
                .UseNLog()
                .Build();
    }
}
