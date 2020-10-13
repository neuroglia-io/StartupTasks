using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Neuroglia.StartupTasks
{

    public static class IServiceCollectionExtensions
    {

        public static IServiceCollection AddStartupTask<TTask>(this IServiceCollection services)
            where TTask: class, IStartupTask
        {
            services.AddHostedService<TTask>();
            services.TryAddSingleton<IStartupTaskManager, StartupTaskManager>();
            return services;
        }

    }

}
