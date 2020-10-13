using Microsoft.Extensions.Hosting;
using System;

namespace Neuroglia.StartupTasks
{

    /// <summary>
    /// Defines the fundamentals of a startup task
    /// </summary>
    public interface IStartupTask
        : IHostedService, IDisposable
    {



    }

}
