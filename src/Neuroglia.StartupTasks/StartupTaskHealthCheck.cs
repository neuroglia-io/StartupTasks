using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.StartupTasks
{

    /// <summary>
    /// Represents an <see cref="IHealthCheck"/> used to check whether or not <see cref="IStartupTask"/>s have run to completion
    /// </summary>
    public class StartupTasksHealthCheck 
        : IHealthCheck
    {

        /// <summary>
        /// Initializes a new <see cref="StartupTasksHealthCheck"/>
        /// </summary>
        /// <param name="startupTaskManager">The service used to manage <see cref="IStartupTask"/>s</param>
        public StartupTasksHealthCheck(IStartupTaskManager startupTaskManager)
        {
            this.StartupTaskManager = startupTaskManager;
        }

        /// <summary>
        /// Gets the service used to manage <see cref="IStartupTask"/>s
        /// </summary>
        protected IStartupTaskManager StartupTaskManager { get; }

        /// <inheritdoc/>
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (this.StartupTaskManager.TasksRanToCompletion)
                return Task.FromResult(HealthCheckResult.Healthy("All startup tasks ran to completion"));
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["RunningTasks"] = this.StartupTaskManager.PendingTasks;
            data["CompletedTasks"] = this.StartupTaskManager.CompletedTasks;
            return Task.FromResult(HealthCheckResult.Unhealthy("Startup tasks are still running", data: data));
        }

    }

}
