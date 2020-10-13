using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Neuroglia.StartupTasks
{

    /// <summary>
    /// Defines extensions for <see cref="IServiceCollection"/>s
    /// </summary>
    public static class IServiceCollectionExtensions
    {

        /// <summary>
        /// Adds and configures the specified <see cref="IStartupTask"/>
        /// </summary>
        /// <typeparam name="TTask">The type of <see cref="IStartupTask"/> to add</typeparam>
        /// <param name="services">The extended <see cref="IServiceCollection"/></param>
        /// <returns>The configured <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddStartupTask<TTask>(this IServiceCollection services)
            where TTask: class, IStartupTask
        {
            services.AddHostedService<TTask>();
            services.TryAddSingleton<IStartupTaskManager, StartupTaskManager>();
            return services;
        }

    }

}
