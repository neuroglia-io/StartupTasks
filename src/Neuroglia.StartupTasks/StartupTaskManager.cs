using System.Collections.Generic;
using System.Linq;

namespace Neuroglia.StartupTasks
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IStartupTaskManager"/> interface
    /// </summary>
    public class StartupTaskManager
        : IStartupTaskManager
    {

        private object _Lock = new object();

        private List<IStartupTaskDescriptor> _PendingTasks = new List<IStartupTaskDescriptor>();
        /// <inheritdoc/>
        public IReadOnlyCollection<IStartupTaskDescriptor> PendingTasks
        {
            get
            {
                return this._PendingTasks.AsReadOnly();
            }
        }

        private List<IStartupTaskDescriptor> _CompletedTasks = new List<IStartupTaskDescriptor>();
        /// <inheritdoc/>
        public IReadOnlyCollection<IStartupTaskDescriptor> CompletedTasks
        {
            get
            {
                return this._CompletedTasks.AsReadOnly();
            }
        }

        /// <inheritdoc/>
        public bool TasksRanToCompletion
        {
            get
            {
                return !this._PendingTasks.Any();
            }
        }

        /// <inheritdoc/>
        public void Register(IStartupTask task)
        {
            lock (this._Lock)
            {
                this._PendingTasks.Add(new StartupTaskDescriptor(task));
            }
        }

        /// <inheritdoc/>
        public void MarkAsCompleted(IStartupTask task)
        {
            lock (this._Lock)
            {
                IStartupTaskDescriptor descriptor = this.PendingTasks.FirstOrDefault(t => t.Task == task);
                this._PendingTasks.Remove(descriptor);
                this._CompletedTasks.Add(descriptor);
            }
        }

    }

}
