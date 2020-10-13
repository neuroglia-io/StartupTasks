using System;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.StartupTasks
{

    public abstract class StartupTask
        : IStartupTask
    {

        private Task _ExecutingTask;
        private bool _Disposed;
        private readonly CancellationTokenSource _CancellationTokenSource = new CancellationTokenSource();

        protected StartupTask(IStartupTaskManager startupTaskManager)
        {
            this.StartupTaskManager = startupTaskManager;
        }

        protected IStartupTaskManager StartupTaskManager { get; }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.StartupTaskManager.Register(this);
            this._ExecutingTask = this.ExecuteAsync(cancellationToken).ContinueWith((t) => this.StartupTaskManager.MarkAsCompleted(this));
            if (this._ExecutingTask.IsCompleted)
                return this._ExecutingTask;
            return Task.CompletedTask;
        }

        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);

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

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }

}
