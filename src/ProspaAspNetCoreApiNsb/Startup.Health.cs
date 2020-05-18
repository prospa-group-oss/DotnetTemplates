using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Prospa.Extensions.Hosting;
using ProspaAspNetCoreApiNsb.Application.HealthChecks;

namespace ProspaAspNetCoreApiNsb
{
    public static class StartupHealth
    {
        public static IApplicationBuilder UseDefaultHealth(this IApplicationBuilder builder)
        {
            builder.UseHealthChecks(
                Constants.HealthEndpoint,
                new HealthCheckOptions { ResponseWriter = ProspaConstants.WriteHealthResponse });

            return builder;
        }

        public static IServiceCollection AddDefaultHealth(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck<SampleHealthCheck>("sample_health_check");

            return services;
        }
    }
}
