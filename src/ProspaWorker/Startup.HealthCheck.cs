using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prospa.Extensions.Diagnostics.DDPublisher;
using Prospa.Extensions.Hosting;

namespace ProspaWorker
{
    public static class StartupHealthCheck
    {
        public static IServiceCollection SetupHealthCheck(this IServiceCollection services, HostBuilderContext context)
        {
            var dataDogApiKey = context.Configuration.GetValue<string>("DataDogApiKey");
            var dataDogAppKey = context.Configuration.GetValue<string>("DataDogAppKey");
            var appDomain = context.Configuration.GetValue<string>("AppDomain");

            var healthCheckBuilder = services.AddHealthChecks()
                .AddApplicationInsightsPublisher();

            if (!string.IsNullOrWhiteSpace(dataDogApiKey) &&
                !string.IsNullOrWhiteSpace(dataDogAppKey))
            {
                healthCheckBuilder.AddDatadogPublisher(configuration =>
                {
                    configuration.Domain = appDomain;
                    configuration.Application = typeof(Program).Assembly.GetName().Name;
                    configuration.Environment = ProspaConstants.Environments.CurrentEnv;
                    configuration.ServiceCheckName = typeof(Program).Assembly.GetName().Name;
                    configuration.ApiKey = dataDogApiKey;
                    configuration.ApplicationKey = dataDogAppKey;
                    configuration.Url = "https://api.datadoghq.com/api";
                });
            }

            return services;
        }
    }
}
