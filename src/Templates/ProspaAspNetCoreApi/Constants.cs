﻿using System;
using Microsoft.AspNetCore.HttpOverrides;

namespace ProspaAspNetCoreApi
{
    public static class Constants
    {
        public const string KeyVaultName = "";

        public static class Auth
        {
            public const string EndpointKey = nameof(EndpointKey);

            public static class Policies
            {
                public const string ReadPolicy = "Read";
                public const string WritePolicy = "Write";
            }
        }

        public static class ConfigurationKeys
        {
            public static class AppInsights
            {
                public const string InstrumentationKey = "APPINSIGHTS_INSTRUMENTATIONKEY";
            }

            public static class Seq
            {
                public const string SeqServerUrl = nameof(SeqServerUrl);
            }
        }

        public static class Cors
        {
            public const string AllowAny = nameof(AllowAny);
        }

        public static class Environments
        {
            public const string Development = nameof(Development);
            public const string Production = nameof(Production);
            public const string Staging = nameof(Staging);
            public static readonly string CurrentAspNetCoreEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            public static string Prefix()
            {
                if (IsDevelopment())
                {
                    return "demo-";
                }

                if (IsStaging())
                {
                    return "staging-";
                }

                if (IsProduction())
                {
                    return "live-";
                }

                throw new ApplicationException("Invalid ASPNETCORE_ENVIRONMENT");
            }

            public static bool IsDevelopment() { return CurrentAspNetCoreEnv == Development; }

            public static bool IsStaging() { return CurrentAspNetCoreEnv == Staging; }

            public static bool IsProduction() { return CurrentAspNetCoreEnv == Production; }
        }

        public static class HttpHeaders
        {
            public const int HstsMaxAgeDays = 18 * 7;
            public const ForwardedHeaders ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All;
        }

        public static class Versioning
        {
            public const string GroupNameFormat = "'v'V";
        }
    }
}