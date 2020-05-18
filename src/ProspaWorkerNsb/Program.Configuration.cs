using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ProspaWorkerNsb
{
    public static class ProgramConfiguration
    {
        public static IHostBuilder ConfigureDefaultAppConfiguration(this IHostBuilder hostBuilder)
        {
            if (hostBuilder == null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            hostBuilder.ConfigureAppConfiguration(
                (context, builder) =>
                {
                    AddDefaultAzureKeyVault(builder, context.HostingEnvironment);
                    builder.AddSharedKeyvault();
                    builder.AddSharedAppConfiguration();
                });

            return hostBuilder;
        }

        private static void AddDefaultAzureKeyVault(IConfigurationBuilder builder, IHostEnvironment hostEnvironment)
        {
            var builtConfig = builder.Build();
            var keyVaultName = builtConfig.GetValue<string>("keyVaultName");

            if (keyVaultName == null)
            {
                return;
            }

            var keyVaultEndpoint = $"https://{ProspaWorker.Constants.Environment.Prefix(hostEnvironment)}{keyVaultName}.vault.azure.net/";
            builder.AddAzureKeyVault(keyVaultEndpoint);
        }
    }
}
