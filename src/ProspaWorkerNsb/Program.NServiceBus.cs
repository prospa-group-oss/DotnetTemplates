using System;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using NServiceBus;
using NServiceBus.Json;
using NServiceBus.Logging;
using NServiceBus.Pipeline;
using NServiceBus.Serilog;
using Prospa.Extensions.Hosting;

// ReSharper disable CheckNamespace
namespace Microsoft.Extensions.Hosting
    // ReSharper restore CheckNamespace
{
    public static class ProgramNServiceBus
    {
        public static IHostBuilder UseDefaultNServiceBus(this IHostBuilder builder)
        {
            return builder.UseNServiceBus(context =>
            {
                var connectionString = context.Configuration.SharedAzureServiceBusConnection();

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new ApplicationException("ServiceBus Connection String must be provided");
                }

                var connection = new ServiceBusConnectionStringBuilder(connectionString);

                var endpointName = GetEndpointName(context.HostingEnvironment);
                var cfg = new EndpointConfiguration(endpointName);

                cfg.License(context.Configuration.GetValue<string>(ProspaConstants.SharedConfigurationKeys.NServiceBusLicense));
                cfg.SendFailedMessagesTo("error");
                cfg.AuditProcessedMessagesTo("audit");
                cfg.EnableInstallers();

                cfg.UseSerialization<SystemJsonSerializer>();

                cfg.CustomDiagnosticsWriter(diagnostics => Task.CompletedTask);
                cfg.Recoverability()
                    .Immediate(c => c.NumberOfRetries(1))
                    .Delayed(c => c.NumberOfRetries(5).TimeIncrease(TimeSpan.FromSeconds(2)));

                var pipeline = cfg.Pipeline;
                pipeline.StripAssemblyVersionFromEnclosedMessageTypePipeline();

                var transport = cfg.UseTransport<AzureServiceBusTransport>();
                transport.ConnectionString(connection.ToString);

                transport.Routing();

                LogManager.Use<SerilogFactory>();
                var serilog = cfg.EnableSerilogTracing();
                serilog.EnableMessageTracing();
                serilog.EnableSagaTracing();

                return cfg;
            });
        }

        private static string GetEndpointName(IHostEnvironment hostEnvironment)
        {
            var endpointName = "prospaworkernsb";
            if (hostEnvironment.IsDevelopment())
            {
                endpointName = $"prospaworkernsb.{Environment.MachineName.ToLower()}";
            }

            return endpointName;
        }
    }
}
