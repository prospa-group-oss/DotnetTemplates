﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ProspaAspNetCoreApi;

// ReSharper disable CheckNamespace
namespace Microsoft.AspNetCore.Builder
// ReSharper restore CheckNamespace
{
    public static class StartupSecurityHeaders
    {
        public static IApplicationBuilder UseDefaultSecurityHeaders(this IApplicationBuilder app, IHostEnvironment hostingEnvironment)
        {
            if (!hostingEnvironment.IsDevelopment())
            {
                app.UseHsts(options => options.MaxAge(days: Constants.HttpHeaders.HstsMaxAgeDays).IncludeSubdomains().Preload().AllResponses());
                app.UseXfo(options => options.SameOrigin());
            }

            return app;
        }
    }
}