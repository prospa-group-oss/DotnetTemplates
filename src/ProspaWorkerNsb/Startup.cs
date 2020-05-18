using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ProspaWorkerNsb
{
    public static class Startup
    {
        public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddApplicationInsightsTelemetryWorkerService();
        }
    }
}
