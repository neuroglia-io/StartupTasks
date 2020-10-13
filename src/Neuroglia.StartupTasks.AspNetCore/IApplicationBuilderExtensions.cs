using Microsoft.AspNetCore.Builder;

namespace Neuroglia.StartupTasks
{

    public static class IApplicationBuilderExtensions
    {

        public static IApplicationBuilder UseStartupTasks(this IApplicationBuilder application)
        {
            application.UseMiddleware<StartupTaskMiddleware>();
            return application;
        }

    }

}
