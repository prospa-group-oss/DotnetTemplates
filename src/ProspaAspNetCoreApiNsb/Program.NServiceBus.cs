using System;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NServiceBus;
using NServiceBus.Json;
using NServiceBus.Logging;
using NServiceBus.Pipeline;
using NServiceBus.Serilog;
using Prospa.Extensions.Hosting;
using V1.Commands;

namespace ProspaAspNetCoreApiNsb
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

                var endpointName = GetEndpointName();
                var cfg = new EndpointConfiguration(endpointName);

                cfg.License(context.Configuration.GetNServiceBusLicense());
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

                var routing = transport.Routing();
                RouteSampleCommands(routing);

                LogManager.Use<SerilogFactory>();
                var serilog = cfg.EnableSerilogTracing();
                serilog.EnableMessageTracing();
                serilog.EnableSagaTracing();

                return cfg;
            });
        }

        private static string GetEndpointName()
        {
            var endpointName = "prospaaspnetcoreapinsb";
            if (ProspaConstants.Environments.IsDevelopment)
            {
                endpointName = $"prospaaspnetcoreapinsb.{Environment.MachineName.ToLower()}";
            }

            return endpointName;
        }

        private static void RouteSampleCommands(RoutingSettings<AzureServiceBusTransport> routing)
        {
            var endpointName = "prospaworkernsb";

            if (ProspaConstants.Environments.IsDevelopment)
            {
                endpointName += $".{Environment.MachineName.ToLower()}";
            }

            routing.RouteToEndpoint(typeof(SampleCommand), endpointName);
        }
    }
}
