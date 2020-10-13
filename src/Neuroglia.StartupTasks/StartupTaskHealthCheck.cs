using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.StartupTasks
{

    public class StartupTasksHealthCheck 
        : IHealthCheck
    {

        public StartupTasksHealthCheck(IStartupTaskManager startupTaskManager)
        {
            this.StartupTaskManager = startupTaskManager;
        }

        protected IStartupTaskManager StartupTaskManager { get; }

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
