﻿using System;
using Microsoft.AspNetCore.HttpOverrides;

namespace ProspaAspNetCoreApi
{
    public static class Constants
    {
        public static class Auth
        {
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

            public static bool IsDevelopment() { return CurrentAspNetCoreEnv == Development; }
        }

        public static class HttpHeaders
        {
            public const int HstsMaxAgeDays = 18 * 7;
            public const ForwardedHeaders ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All;
        }

        public const string EndpointKey = "67EC9975-1F83-4FF9-A7D9-B6C64B652361";

        public static class Versioning
        {
            public const string GroupNameFormat = "'v'V";
        }
    }
}