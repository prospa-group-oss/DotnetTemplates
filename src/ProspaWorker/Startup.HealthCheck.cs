using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ProspaWorker
{
    public static class StartupHealthCheck
    {
        public static IServiceCollection SetupHealthCheck(this IServiceCollection services, HostBuilderContext context)
        {
            var dataDogApiKey = context.Configuration.GetValue<string>("DataDogApiKey");

            var healthCheckBuilder = services.AddHealthChecks()
                .AddApplicationInsightsPublisher();

            if (!string.IsNullOrWhiteSpace(dataDogApiKey))
            {
                healthCheckBuilder.AddDatadogPublisher(
                    typeof(Program).Assembly.GetName().Name,
                    defaultTags: new[] { $"env:{context.HostingEnvironment.EnvironmentName}", "p3domain:<<app-domain>>", "p3app:<<app-name>>" });
            }

            return services;
        }
    }
}
