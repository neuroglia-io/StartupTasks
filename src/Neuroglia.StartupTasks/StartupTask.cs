using System;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.StartupTasks
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IStartupTask"/> interface
    /// </summary>
    public abstract class StartupTask
        : IStartupTask
    {

        private Task _ExecutingTask;
        private bool _Disposed;
        private readonly CancellationTokenSource _CancellationTokenSource = new CancellationTokenSource();

        /// <summary>
        /// Initializes a new <see cref="StartupTask"/>
        /// </summary>
        /// <param name="startupTaskManager">The service used to manage <see cref="IStartupTask"/>s</param>
        protected StartupTask(IStartupTaskManager startupTaskManager)
        {
            this.StartupTaskManager = startupTaskManager;
        }

        /// <summary>
        /// Gets the service used to manage <see cref="IStartupTask"/>s
        /// </summary>
        protected IStartupTaskManager StartupTaskManager { get; }

        /// <inheritdoc/>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.StartupTaskManager.Register(this);
            this._ExecutingTask = this.ExecuteAsync(cancellationToken).ContinueWith((t) => this.StartupTaskManager.MarkAsCompleted(this));
            return Task.CompletedTask;
        }

        /// <summary>
        /// Executes the <see cref="StartupTask"/>'s work
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);

        /// <inheritdoc/>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (this._ExecutingTask == null)
                return;
            try
            {
                this._CancellationTokenSource.Cancel();
            }
            finally
            {
                await Task.WhenAny(this._ExecutingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        /// <summary>
        /// Disposes of the <see cref="StartupTask"/>
        /// </summary>
        /// <param name="disposing">A boolean indicating whether or not the <see cref="StartupTask"/> is being disposed of</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._Disposed)
            {
                if (disposing)
                {
                    this._CancellationTokenSource.Cancel();
                }
                this._Disposed = true;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }

}
