using System;
using Microsoft.Extensions.Hosting;

// ReSharper disable CheckNamespace
namespace ProspaWorker
    // ReSharper restore CheckNamespace
{
    public static class Constants
    {
        public static class Environment
        {
            public static string Prefix(IHostEnvironment hostEnvironment)
            {
                if (hostEnvironment.IsDevelopment()
                    || hostEnvironment.IsStaging())
                {
                    return "staging-";
                }

                if (hostEnvironment.IsProduction())
                {
                    return "live-";
                }

                throw new InvalidOperationException("Invalid DEPLOYMENT_ENVIRONMENT");
            }
        }

        public static class ConnectionStrings
        {
            public static readonly string ServiceBus = nameof(ServiceBus);
        }
    }
}
