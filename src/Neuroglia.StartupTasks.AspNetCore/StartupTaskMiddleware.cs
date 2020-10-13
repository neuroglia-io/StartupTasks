using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Neuroglia.StartupTasks
{

    public class StartupTaskMiddleware
    {

        public StartupTaskMiddleware(IStartupTaskManager startupTaskManager, RequestDelegate next)
        {
            this.StartupTaskManager = startupTaskManager;
            this.Next = next;
        }

        protected IStartupTaskManager StartupTaskManager { get; }

        protected RequestDelegate Next { get; }

        public async Task Invoke(HttpContext httpContext)
        {
            if (this.StartupTaskManager.TasksRanToCompletion)
            {
                await this.Next(httpContext);
                return;
            }
            httpContext.Response.StatusCode = 503;
            httpContext.Response.Headers["Retry-After"] = "30";
            await httpContext.Response.WriteAsync("Service Unavailable");
        }

    }

}
