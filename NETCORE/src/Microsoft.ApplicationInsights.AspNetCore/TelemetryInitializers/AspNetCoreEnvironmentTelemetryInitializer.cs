﻿namespace Microsoft.ApplicationInsights.AspNetCore.TelemetryInitializers
{
    using Microsoft.ApplicationInsights.Channel;
    using Microsoft.ApplicationInsights.DataContracts;
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    /// <see cref="ITelemetryInitializer"/> implementation that stamps ASP.NET Core environment name
    /// on telemetries.
    /// </summary>
    public class AspNetCoreEnvironmentTelemetryInitializer : ITelemetryInitializer
    {
        private const string AspNetCoreEnvironmentPropertyName = "AspNetCoreEnvironment";
#if NET8_0_OR_GREATER
        private readonly IWebHostEnvironment environment;
#else
        private readonly IHostingEnvironment environment;
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="AspNetCoreEnvironmentTelemetryInitializer"/> class.
        /// </summary>
        /// <param name="environment">HostingEnvironment to provide EnvironmentName to be added to telemetry properties.</param>
        public AspNetCoreEnvironmentTelemetryInitializer(
#if NET8_0_OR_GREATER
            IWebHostEnvironment environment)
#else
            IHostingEnvironment environment)
#endif
        {
            this.environment = environment;
        }

        /// <inheritdoc />
        public void Initialize(ITelemetry telemetry)
        {
            if (this.environment != null)
            {
                if (telemetry is ISupportProperties telProperties && !telProperties.Properties.ContainsKey(AspNetCoreEnvironmentPropertyName))
                {
                    telProperties.Properties.Add(
                        AspNetCoreEnvironmentPropertyName,
                        this.environment.EnvironmentName);
                }
            }
        }
    }
}
