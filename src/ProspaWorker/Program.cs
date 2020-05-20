using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ProspaWorker
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            try
            {
                var host = Host
                    .CreateDefaultBuilder(args)
                    .ConfigureAppConfiguration((context, config) =>
                    {
                        config.AddSharedAppConfiguration();
                    })
                    .UseSerilog((context, configuration) =>
                    {
                        context.CreateProspaDefaultLogger(configuration, typeof(Program));
                    })
                    .ConfigureServices(Startup.ConfigureServices)
                    .UseConsoleLifetime()
                    .Build();

                // Doesn't seem to log unless you create a TC
                host.Services.GetService(typeof(TelemetryClient));

                await host.RunAsync();

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Host terminated unexpectedly, check the application's Host configuration.");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Demystify());
                Log.Fatal(e, "Host terminated unexpectedly, check the application's Host configuration.");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
