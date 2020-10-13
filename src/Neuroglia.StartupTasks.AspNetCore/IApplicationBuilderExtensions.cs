using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Neuroglia.StartupTasks
{

    /// <summary>
    /// Defines extensions for <see cref="IApplicationBuilder"/>s
    /// </summary>
    public static class IApplicationBuilderExtensions
    {

        /// <summary>
        /// Adds and configures the <see cref="StartupTaskMiddleware"/>, used for <see cref="IStartupTask"/>-related <see cref="IHealthCheck"/>s
        /// </summary>
        /// <param name="application">The <see cref="IApplicationBuilder"/> to configre</param>
        /// <returns>The configured <see cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseStartupTasks(this IApplicationBuilder application)
        {
            application.UseMiddleware<StartupTaskMiddleware>();
            return application;
        }

    }

}
