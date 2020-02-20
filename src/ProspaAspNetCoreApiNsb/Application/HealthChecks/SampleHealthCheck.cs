﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ProspaAspNetCoreApiNsb.Application.HealthChecks
{
    public class SampleHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var nowSecs = DateTime.UtcNow.Second;

            if (nowSecs % 2 == 0)
            {
                return Task.FromResult(HealthCheckResult.Healthy());
            }

            if (nowSecs % 3 == 0)
            {
                return Task.FromResult(HealthCheckResult.Degraded());
            }

            return Task.FromResult(HealthCheckResult.Unhealthy());
        }
    }
}
