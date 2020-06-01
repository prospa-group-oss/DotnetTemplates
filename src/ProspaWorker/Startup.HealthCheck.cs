using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prospa.Extensions.Diagnostics.DDPublisher;

namespace ProspaWorker
{
    public static class StartupHealthCheck
    {
        public static IServiceCollection SetupHealthCheck(this IServiceCollection services, HostBuilderContext context)
        {
            var dataDogApiKey = context.Configuration.GetValue<string>("DataDogApiKey");
            var dataDogAppKey = context.Configuration.GetValue<string>("DataDogAppKey");

            var healthCheckBuilder = services.AddHealthChecks()
                .AddApplicationInsightsPublisher();

            if (!string.IsNullOrWhiteSpace(dataDogApiKey) &&
                !string.IsNullOrWhiteSpace(dataDogAppKey))
            {
                healthCheckBuilder.AddDatadogPublisher(configuration =>
                {
                    configuration.ServiceCheckName = typeof(Program).Assembly.GetName().Name;
                    configuration.ApiKey = dataDogApiKey;
                    configuration.ApplicationKey = dataDogAppKey;
                    configuration.Url = "https://api.datadoghq.com/api";
                    configuration.DefaultTags = new[]
                    {
                        $"env:{context.HostingEnvironment.EnvironmentName}",
                        "p3domain:<<app-domain>>",
                        "p3app:<<app-name>>"
                    };
                });
            }

            return services;
        }
    }
}
