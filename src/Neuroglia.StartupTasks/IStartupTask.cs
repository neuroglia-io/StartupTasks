using Microsoft.Extensions.Hosting;
using System;

namespace Neuroglia.StartupTasks
{

    public interface IStartupTask
        : IHostedService, IDisposable
    {



    }

}
