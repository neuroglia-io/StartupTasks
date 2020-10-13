using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Neuroglia.StartupTasks
{

    /// <summary>
    /// Represents a middleware used to interrupt the request pipeline's execution while <see cref="IStartupTask"/>s are running
    /// </summary>
    public class StartupTaskMiddleware
    {

        /// <summary>
        /// Initializes a new <see cref="StartupTaskMiddleware"/>
        /// </summary>
        /// <param name="startupTaskManager">The service used to manage <see cref="IStartupTask"/>s</param>
        /// <param name="next">The next <see cref="RequestDelegate"/> in the pipeline</param>
        public StartupTaskMiddleware(IStartupTaskManager startupTaskManager, RequestDelegate next)
        {
            this.StartupTaskManager = startupTaskManager;
            this.Next = next;
        }

        /// <summary>
        /// Gets the service used to manage <see cref="IStartupTask"/>s
        /// </summary>
        protected IStartupTaskManager StartupTaskManager { get; }

        /// <summary>
        /// Gets the next <see cref="RequestDelegate"/> in the pipeline
        /// </summary>
        protected RequestDelegate Next { get; }

        /// <summary>
        /// Invokes the middleware
        /// </summary>
        /// <param name="httpContext">The current <see cref="HttpContext"/></param>
        /// <returns>A new awaitable <see cref="Task"/></returns>
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
